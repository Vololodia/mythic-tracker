using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MythicTracker.Application.GameStateObserver
{
    public class LogObserver : IGameStateObserver
    {
        private string _logDirectory;
        private List<string> _newLogLine = new List<string>();
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private int _lastReadLineNumber = 1;
        public LogObserver(string Log)
        {
            _logDirectory = Log;
        }
        private void RunLogChangeWatcher( )
        {
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
            RunLogStreamReader();
        }
        private void RunLogStreamReader()
        {
            using (StreamReader sr = new StreamReader(_logDirectory))
            {
                for(int i = 1; i <= _lastReadLineNumber; i++)
                {
                    sr.ReadLine();
                }
                do
                {
                    _newLogLine.Add(sr.ReadLine());
                    _lastReadLineNumber++;
                }
                while (sr.ReadLine() != null);
            }
        }
        public void Finish()
        {
            watcher.Dispose();
        }
        public void Start()
        {
            RunLogChangeWatcher();
        }
    }
}
