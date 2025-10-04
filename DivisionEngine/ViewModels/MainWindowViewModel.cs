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
    }
}
