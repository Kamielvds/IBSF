using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    public static class Saving
    {
        private static string Path => Program.Path;
        private static Activities Activities => Program.Activities;
        
        
        /// <summary>
        /// Save the file based on the Language stored in _activity's
        /// </summary>
        public static void SaveFile()
        {
            if (Path == null)
            {
                Errors.NoFileLoaded();
                return;
            }

            Activities.SaveFile();
        }
    }
}