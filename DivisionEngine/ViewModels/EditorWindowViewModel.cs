using ReactiveUI;

namespace DivisionEngine.Editor.ViewModels
{
    public partial class EditorWindowViewModel : ViewModelBase
    {
        private string title = "Untitled";
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
    }
}
