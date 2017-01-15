using SlientAway.Backend;

namespace SlientAway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly TrayIcon _tray;

        public MainWindow()
        {
            InitializeComponent();

            _tray = new TrayIcon();
            _tray.Show();

            var hooks = new WindowsHooks();
            // ReSharper disable once ObjectCreationAsStatement
            new SessionHandler(hooks);
        }
    }
}