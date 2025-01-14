using System;

namespace Base.Events
{
    public static class EventsFader
    {
        public static Action<bool, Action> DoFade;
    }
}