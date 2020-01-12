using System;
using System.Collections.Generic;
using System.IO;

namespace MythicTracker.Application.GameStateObserver
{
    public class LogObserver : IGameStateObserver
    {
        private readonly string _filepath;
        private readonly List<string> _newLogLines = new List<string>();
        private FileSystemWatcher _watcher;
        private int _lastReadLineNumber = 0;

        public LogObserver(string filepath)
        {
            _filepath = filepath;
        }

        private void RunLogChangeWatcher()
        {
            if (_watcher != null)
            {
                return;
            }

            _watcher = new FileSystemWatcher()
            {
                Path = Path.GetDirectoryName(_filepath),
                Filter = Path.GetFileName(_filepath),
                NotifyFilter = NotifyFilters.LastWrite,
            };

            _watcher.Created += OnChanged;
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        public event EventHandler<GameStateChangedEventArgs> Notify;

        protected virtual void OnChanged(object source, FileSystemEventArgs e)
        {
            // Notify?.Invoke();
            RunLogStreamReader();
        }

        private void RunLogStreamReader()
        {
            using (StreamReader sr = new StreamReader(_filepath))
            {
                for (int i = 1; i < _lastReadLineNumber; i++)
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
            _watcher.Dispose();
            _watcher = null;
        }

        public void Start()
        {
            RunLogChangeWatcher();
        }
    }
}
