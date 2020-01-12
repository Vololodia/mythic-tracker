using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MythicTracker.Application.GameStateObserver
{
    public class LogObserver : IGameStateObserver
    {
        private string _logDirectory { get; set; }
        ArrayList _logStream = new ArrayList();
        private int _lastReadLineNumber { get; set; }
        public LogObserver(string Log)
        {
            _logDirectory = Log;
        }
        private void RunLogChangeWatcher( )
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = _logDirectory;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }
        public event EventHandler Notify;
        protected virtual void OnChanged(object source, FileSystemEventArgs e)
        {
           // Notify?.Invoke();
        }
        private void RunLogStreamReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_logDirectory))
                {
                    for(int i = 1; i <= _lastReadLineNumber; i++)
                    {
                        sr.ReadLine();
                    }
                    _logStream.Add(sr.ReadLine());
                    _lastReadLineNumber++;
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
