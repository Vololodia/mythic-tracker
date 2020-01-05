using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.ConsoleApp
{
    public interface IGameStateObserver
    {
        event GameStateChangedEventArgs Notify;
        
        public void Start()
        {

        }

        public void Finish()
        {

        }
    }
}
