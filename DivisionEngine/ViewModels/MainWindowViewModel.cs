using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DivisionEngine.Editor.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<EditorWindowViewModel> CenterTabs { get; } = [];
        public ObservableCollection<EditorWindowViewModel> BottomTabs { get; } = [];
        public ObservableCollection<EditorWindowViewModel> LeftTabs { get; } = [];
        public ObservableCollection<EditorWindowViewModel> RightTabs { get; } = [];

        public MainWindowViewModel()
        {
            LeftTabs.Add(new WorldWindowViewModel());

            CenterTabs.Add(new EnvironmentWindowViewModel());

            RightTabs.Add(new PropertiesWindowViewModel());

            BottomTabs.Add(new AssetsWindowViewModel());
            BottomTabs.Add(new ConsoleWindowViewModel());
        }

        [RelayCommand]
        private void AddWindowToTab(string window)
        {
            EditorWindowViewModel? vm = window switch
            {
                "Assets" => new AssetsWindowViewModel(),
                "Console" => new ConsoleWindowViewModel(),
                "Environment" => new EnvironmentWindowViewModel(),
                "Properties" => new PropertiesWindowViewModel(),
                "World" => new WorldWindowViewModel(),
                _ => null
            };

            if (vm == null)
            {
                Debug.Error($"Unknown window type: {window}");
                return;
            }

            CenterTabs.Add(vm);
        }
    }
}
