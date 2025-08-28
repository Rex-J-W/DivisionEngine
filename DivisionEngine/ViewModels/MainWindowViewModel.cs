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
            BottomTabs.Add(new ConsoleWindowViewModel());
            CenterTabs.Add(new WorldWindowViewModel());
            LeftTabs.Add(new ConsoleWindowViewModel());
            RightTabs.Add(new ConsoleWindowViewModel());

            BottomTabs.Add(new ConsoleWindowViewModel() { Title = "Test" });
        }
    }
}
