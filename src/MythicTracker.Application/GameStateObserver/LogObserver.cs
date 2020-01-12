using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MythicTracker.Application.GameStateObserver
{
    public class LogObserver : IGameStateObserver
    {
        private readonly string _logDirectory;
        private List<string> _newLogLines = new List<string>();
        private FileSystemWatcher watcher;
        private int _lastReadLineNumber = 1;
        public LogObserver(string Log)
        {
            _logDirectory = Log;
        }

        private void RunLogChangeWatcher( )
        {
            if (watcher == null)
            {
                watcher = new FileSystemWatcher()
                {
                    Path = _logDirectory,
                    NotifyFilter = NotifyFilters.LastWrite
                };

                watcher.Created += OnChanged;
                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;
            }

            else
            {
                return; 
            }

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

                while (sr.ReadLine() != null)
                {
                    _newLogLines.Add(sr.ReadLine());
                    _lastReadLineNumber++;
                }

            }

        }
        public void Finish()
        {
            watcher.Dispose();
            watcher = null;
        }

        public void Start()
        {
            RunLogChangeWatcher();
        }

    }
}
