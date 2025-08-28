using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using DivisionEngine.Editor.ViewModels;
using Silk.NET.Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DivisionEngine.Editor
{
    public partial class App : Application
    {
        /// <summary>
        /// Reference to the Division SDF render pipeline.
        /// </summary>
        public static RenderPipeline? Renderer { get; private set; }
        public static Input? InputSystem { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };

                // Start the SDFRenderer in a separate thread
                Renderer = new RenderPipeline();
                Task.Run(() => Renderer.Run(true));

                Renderer.Close += () =>
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        desktop.Shutdown();
                        Environment.Exit(0);
                    });
                };

                // Initialize the input system
                InputSystem = new Input();
                SetupInput(desktop);

                // Close the renderer window when the application exits
                desktop.Exit += (_, _) =>
                {
                    Renderer?.RendererWindow?.Close();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Sets up input handling for the Division Engine editor.
        /// </summary>
        /// <param name="desktop">The desktop application lifetime for Avalonia UI.</param>
        private static async void SetupInput(IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avalonia input handling
            desktop.MainWindow!.KeyUp += (s, e) => InputSystem?.SetKeyUp(EditorInput.AvaloniaToKeyCode(e.Key));
            desktop.MainWindow.KeyDown += (s, e) => InputSystem?.SetKeyDown(EditorInput.AvaloniaToKeyCode(e.Key));

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
                    keyboard.KeyDown += (kb, key, code) => InputSystem!.SetKeyDown(EditorInput.SilkNetToKeyCode(key));
                    keyboard.KeyUp += (kb, key, code) => InputSystem!.SetKeyUp(EditorInput.SilkNetToKeyCode(key));
                }
            }
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}