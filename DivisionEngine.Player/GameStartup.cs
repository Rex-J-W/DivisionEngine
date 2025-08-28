using Silk.NET.Input;

namespace DivisionEngine.Player;

/// <summary>
/// Represents the main entry point for the game application.
/// </summary>
public class GameStartup
{
    public static RenderPipeline? Renderer { get; private set; }
    public static Input? InputSystem { get; private set; }

    /// <summary>
    /// The main entry point for the game.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    [STAThread]
    public static void Main(string[] args)
    {
        InputSystem = new Input();
        SetupInput();

        Renderer = new RenderPipeline();
        Renderer.Run(false);
    }

    /// <summary>
    /// Configures the input system for the player build.
    /// </summary>
    private static async void SetupInput()
    {
        // Silk.NET input handling
        while (Renderer == null || Renderer!.RendererWindow == null)
            await Task.Delay(1); // Wait for the renderer to load

        Renderer!.RendererWindow.Load += SilkNetInputSetup;
    }

    private static void SilkNetInputSetup()
    {
        lock (Renderer!.SyncLock)
        {
            IInputContext? input = Renderer!.RendererWindow!.CreateInput();
            foreach (var keyboard in input.Keyboards)
            {
                keyboard.KeyDown += (kb, key, code) => InputSystem!.SetKeyDown(PlayerInput.SilkNetToKeyCode(key));
                keyboard.KeyUp += (kb, key, code) => InputSystem!.SetKeyUp(PlayerInput.SilkNetToKeyCode(key));
            }
        }
    }
}