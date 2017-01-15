using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Application = System.Windows.Application;

namespace SlientAway
{
    public class TrayIcon
    {
        private readonly NotifyIcon _icon;

        public TrayIcon()
        {
            _icon = new NotifyIcon();
        }

        public void Show()
        {
            _icon.Icon = SystemIcons.Application;
            _icon.Visible = true;
            _icon.ContextMenu = CreateContextMenu();
        }

        private ContextMenu CreateContextMenu()
        {
            var items = new[]
            {
                new MenuItem($"SlientAway v{Assembly.GetExecutingAssembly().GetName().Version}") {Enabled = false},
                new MenuItem("Exit", ExitClick),
            };
            var menu = new ContextMenu(items);
            return menu;
        }

        private void ExitClick(object sender, EventArgs eventArgs)
        {
            Application.Current.Shutdown();
        }
    }
}