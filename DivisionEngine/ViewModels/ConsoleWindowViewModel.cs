using System.Collections.ObjectModel;

namespace DivisionEngine.Editor.ViewModels
{
    public partial class ConsoleWindowViewModel : EditorWindowViewModel
    {
        public ObservableCollection<string> Logs { get; } = [];

        public ConsoleWindowViewModel()
        {
            Title = "Console";
            Debug.OnLogUpdate += (entry) =>
            {
                Logs.Add(entry.ToString());
            };
        }
    }
}
