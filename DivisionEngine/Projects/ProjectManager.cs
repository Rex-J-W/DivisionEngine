namespace DivisionEngine.Editor.Projects
{
    internal class ProjectManager()
    {
        public string CurProjectPath { get; private set; } = string.Empty;
        public bool IsProjectOpen => !string.IsNullOrEmpty(CurProjectPath);

        public static void LoadProject(string path)
        {
            
        }

        public static void SaveProject()
        {
            
        }
    }
}
