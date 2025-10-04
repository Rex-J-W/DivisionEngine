using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using System;

namespace DivisionEngine.Editor;

public partial class EnvironmentWindow : UserControl
{
    private readonly DispatcherTimer? renderWindowUpdate;

    public EnvironmentWindow()
    {
        InitializeComponent();

        // Initialize the render window update timer
        renderWindowUpdate = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100) // Approximately 10 FPS
        };
        renderWindowUpdate.Tick += (_, _) => UpdateRendererPosition();
        renderWindowUpdate.Start(); // Start the render window tracking update loop
    }

    /// <summary>
    /// Updates the position, size, and visibility of the renderer's window to match the current position and
    /// dimensions of the <see cref="RenderBackground"/> element.
    /// </summary>
    private void UpdateRendererPosition()
    {
        if (RenderVisualizerFrame == null || App.Renderer?.RendererWindow == null)
            return;

        PixelPoint screenPoint = RenderVisualizerFrame.PointToScreen(new Point(0, 0));
        Size size = RenderVisualizerFrame.Bounds.Size;

        App.Renderer.RendererWindow!.Position = new Silk.NET.Maths.Vector2D<int>(screenPoint.X, screenPoint.Y);
        App.Renderer.RendererWindow.Size = new Silk.NET.Maths.Vector2D<int>((int)size.Width, (int)size.Height);
    }
}