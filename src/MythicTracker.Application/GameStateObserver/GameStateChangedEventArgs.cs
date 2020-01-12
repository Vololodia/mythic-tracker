using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public string[] Data { get; private set; }

        public GameStateChangedEventArgs(string[] d)
        {
            Data = d;
        }
    }
}

   