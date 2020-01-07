using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    interface IGameStateObserver
    {
        event EventHandler Notify;
        void Start();
        void Finish();
    }
}
