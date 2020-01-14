using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(List<string> d)
        {
            Data = d;
        }

        public List<string> Data { get; private set; }
    }
}
