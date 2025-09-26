using DivisionEngine.Input;
using DivisionEngine.Rendering;
using Silk.NET.Input;

namespace DivisionEngine.Player;

/// <summary>
/// Represents the main entry point for the game application.
/// </summary>
public class GameStartup
{
    public static RenderPipeline? Renderer { get; private set; }
    public static InputSystem? UserInput { get; private set; }

    /// <summary>
    /// The main entry point for the game.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    [STAThread]
    public static void Main(string[] args)
    {
        UserInput = new InputSystem();
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
                keyboard.KeyDown += (kb, key, code) => UserInput!.SetKeyDown(PlayerInput.SilkNetToKeyCode(key));
                keyboard.KeyUp += (kb, key, code) => UserInput!.SetKeyUp(PlayerInput.SilkNetToKeyCode(key));
            }
        }
    }
}