using System;

namespace SlientAway.Backend
{
    public class SessionChangedEventArgs : EventArgs
    {
        public SessionChangeType ChangeType { get; set; }

        public SessionChangedEventArgs(SessionChangeType changeType)
        {
            ChangeType = changeType;
        }
    }
}