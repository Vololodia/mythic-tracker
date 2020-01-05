using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

namespace MythicTracker.ConsoleApp
{
    class LogObserver:IGameStateObserver
    {
        private string[] LogDirectory;

        public string[] _LogDirectory
        {
            private get { return LogDirectory; }
            set { LogDirectory = value; }
        }
        
        public delegate void GameStateChangedEventArgs(string[] Data);
        public event GameStateChangedEventArgs Notify;


        public LogObserver(string[] LogDirectory)
        {
            _LogDirectory = LogDirectory;
        }
        
        public static string RunLogStreamReader()
        {
            string line;
            try
            {
                using (StreamReader sr = new StreamReader("NameOfOurLog.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        return line;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private static void RunLogChangeWatcher()
        {
            string[] args = Environment.GetCommandLineArgs();

            if(args.Length != 2)
            {
                Console.WriteLine("Usage: Warcher.exe (directory)");
                return;
            }

            using(FileSystemWatcher watcher =  new FileSystemWatcher())
            {
                watcher.Path = args[1];

                watcher.NotifyFilter = NotifyFilters.LastWrite;

                watcher.Filter = "NameOfOurLog.txt";

                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;

                watcher.EnableRaisingEvents = true;
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"Log: {e.FullPath} {e.ChangeType}");
        }
        public void Start()
        {
            RunLogChangeWatcher();
            RunLogStreamReader();
        }

        public void Finish()
        {

        }
    }
}
