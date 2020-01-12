using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public interface IGameStateObserver
    {
        event EventHandler<GameStateChangedEventArgs> Notify;

        void Start();

        void Finish();
    }
}
