using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MythicTracker.Application.GameStateObserver
{
    public class LogObserver : IGameStateObserver
    {
        private readonly string _filepath;
        private StreamReader _streamReader;
        private bool _observingEnabled;

        public LogObserver(string filepath)
        {
            _filepath = filepath;
        }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        public void Finish()
        {
            _observingEnabled = false;
            _streamReader.Dispose();
        }

        public void Start()
        {
            _observingEnabled = true;
            _streamReader = CreateConcurrentReader(_filepath);
            Task.Run(() => RunLogStreamReader());
        }

        private static StreamReader CreateConcurrentReader(string filepath) =>
           new StreamReader(
               new FileStream(
                   filepath,
                   FileMode.Open,
                   FileAccess.Read,
                   FileShare.ReadWrite));

        private void RunLogStreamReader()
        {
            var lines = new List<string>();
            while (_observingEnabled)
            {
                string line = _streamReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    lines.Add(line);
                }

                if ((line == null && lines.Count > 0) || lines.Count == 10)
                {
                    GameStateChanged?.Invoke(this, new GameStateChangedEventArgs(lines.ToArray()));
                    lines.Clear();
                }
            }
        }
    }
}
