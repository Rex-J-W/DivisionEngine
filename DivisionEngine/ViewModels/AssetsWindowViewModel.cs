using System.Collections.ObjectModel;

namespace DivisionEngine.Editor.ViewModels
{
    public partial class AssetsWindowViewModel : EditorWindowViewModel
    {
        public ObservableCollection<Debug.LogEntry> Logs { get; } = [];

        public AssetsWindowViewModel()
        {
            Title = "Assets";
        }
    }
}
