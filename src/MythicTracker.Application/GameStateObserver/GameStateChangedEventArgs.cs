using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(string[] d)
        {
            Data = d;
        }
        public string[] Data { get; private set; }
    }
}
