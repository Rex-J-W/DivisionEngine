using Avalonia;
using Avalonia.Controls;

namespace DivisionEngine.Editor
{
    /// <summary>
    /// Represents the main UI window of the Division Engine editor.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // Initialize the main window components
#if DEBUG
            this.AttachDevTools(); // Enable developer tools in debug mode
#endif
        }

        //private void Slider_ValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
        //{
        //    lock (App.Renderer!.SyncLock)
        //    {
        //        App.Renderer.Time = (float)e.NewValue;
        //    }
        //}
    }
}