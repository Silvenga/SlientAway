using System;

namespace SlientAway.Backend
{
    public class SessionHandler
    {
        private readonly IWindowsHooks _hooks;

        public SessionHandler(IWindowsHooks hooks)
        {
            _hooks = hooks;

            _hooks.AttachHooks();
            _hooks.SessionChanged += HooksOnSessionChanged;
        }

        private void HooksOnSessionChanged(object sender, SessionChangedEventArgs sessionChangedEventArgs)
        {
            switch (sessionChangedEventArgs.ChangeType)
            {
                case SessionChangeType.Lock:
                    _hooks.Mute();
                    break;
                case SessionChangeType.Unlock:
                    _hooks.Unmute();
                    break;
                case SessionChangeType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}