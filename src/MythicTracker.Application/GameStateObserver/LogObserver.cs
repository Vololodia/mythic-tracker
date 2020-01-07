using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MythicTracker.Application.GameStateObserver
{
    class LogObserver : IGameStateObserver
    {
        private static string LogDirectory;
        private static string LogStream;
        public static string _LogDirectory
        {
            get { return LogDirectory; }
            set { LogDirectory = value; }
        }

        public LogObserver(string LogDirectory)
        {
            _LogDirectory = LogDirectory;
        }

        private void RunLogChangeWatcher( )
        {
            string args = _LogDirectory;

            using(FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = args;
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Created += OnChanged;
                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;
            }
        }

        public event EventHandler Notify;

        protected virtual void OnChanged(object source, FileSystemEventArgs e)
        {
            Notify?.Invoke();
        }

        private static void RunLogStreamReader()
        {
            string args = _LogDirectory;
            var lineNumber = 1;
            try
            {
                using (StreamReader sr = new StreamReader(args))
                {
                    for(int i = 1; i <= lineNumber; i++)
                    {
                        LogStream = sr.ReadLine();
                        lineNumber++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void Finish()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            RunLogChangeWatcher();
            throw new NotImplementedException();
        }
    }
}
