using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameStateObserver
{
    public interface IGameStateObserver
    {
        event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        void Start();

        void Finish();
    }
}
