using System;
using Base.Ui;

namespace Base.Events
{
    public static class EventsGame
    {
        public static Action<UIScreenType, bool> OnGameStateChanged;
    }
}