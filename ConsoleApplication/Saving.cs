using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    public class Saving
    {
        private static string Path => Program._path;
        private static Activitys Activitys => Program._activitys;
        
        
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

            Activitys.SaveFile();
        }
    }
}