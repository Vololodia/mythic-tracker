using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public class GameStateChangedEventArgs : EventArgs
    {
        private string[] data;

        public GameStateChangedEventArgs(string[] d)
        {
            Data = d;
        }
        public string[] Data { get; set; }
    }
}
