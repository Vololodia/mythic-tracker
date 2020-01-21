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
        private bool _logReadStreamSwitcher;

        public LogObserver(string filepath)
        {
            _filepath = filepath;
            _streamReader = CreateConcurrentReader(filepath);
        }

        public event EventHandler<GameStateChangedEventArgs> Notify;

        public void Finish()
        {
            _logReadStreamSwitcher = false;
        }

        public void Start()
        {
            _logReadStreamSwitcher = true;
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
            var linesTemp = new List<string>();
            int counter = 0;
            while (_logReadStreamSwitcher)
            {
                string line = _streamReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    linesTemp.Add(line);
                    counter++;
                    if (linesTemp.Count == 10)
                    {
                        Notify?.Invoke(this, new GameStateChangedEventArgs(linesTemp.ToArray()));
                        linesTemp.Clear();
                        counter = 0;
                    }
                }
                else
                {
                    if (counter != 0)
                    {
                        Notify?.Invoke(this, new GameStateChangedEventArgs(linesTemp.ToArray()));
                        counter = 0;

                    }
                }
            }
        }
    }
}
