using System;

using AudioSwitcher.AudioApi.CoreAudio;

using Microsoft.Win32;

namespace SlientAway.Backend
{
    public interface IWindowsHooks : IDisposable
    {
        event EventHandler<SessionChangedEventArgs> SessionChanged;
        void AttachHooks();
        void Mute();
        void Unmute();
    }

    public class WindowsHooks : IWindowsHooks
    {
        private CoreAudioDevice _defaultPlaybackDevice;
        public event EventHandler<SessionChangedEventArgs> SessionChanged;

        public void AttachHooks()
        {
            _defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            SystemEvents.SessionSwitch += OnSessionSwitch;
        }

        private void OnSessionSwitch(object sender, SessionSwitchEventArgs sessionSwitchEventArgs)
        {
            SessionChangeType changeType;
            switch (sessionSwitchEventArgs.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    changeType = SessionChangeType.Lock;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    changeType = SessionChangeType.Unlock;
                    break;
                default:
                    changeType = SessionChangeType.Unknown;
                    break;
            }
            OnSessionChanged(new SessionChangedEventArgs(changeType));
        }

        protected virtual void OnSessionChanged(SessionChangedEventArgs e)
        {
            SessionChanged?.Invoke(this, e);
        }

        public void Mute()
        {
            _defaultPlaybackDevice.Mute(true);
        }

        public void Unmute()
        {
            _defaultPlaybackDevice.Mute(false);
        }

        public void Dispose()
        {
            SystemEvents.SessionSwitch -= OnSessionSwitch;
        }
    }
}