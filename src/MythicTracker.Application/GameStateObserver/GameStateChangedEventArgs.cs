using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    class GameStateChangedEventArgs : EventArgs
    {
        private string[] data;

        public GameStateChangedEventArgs(string[] d)
        {
            Data = d;
        }
        public string[] Data
        {
            get { return data; }
            set { data = value; }
        }

    }
}
