using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(string[] lines)
        {
            Data = lines;
        }

        public string[] Data { get; private set; }
    }
}
