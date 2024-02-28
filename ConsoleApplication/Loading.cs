using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    public abstract class Loading
    {
        
        private static int InputLength => Program.InputLength;
        private static string [] UserInputSplit => Program.UserInputSplit;

        private static Activitys Activitys
        {
            get => Program._activitys;
            set => Program._activitys = value;
        }

        /// <summary>
        /// Loading the sports file, and saving it to _activity's
        /// </summary>
        /// <exception cref="InvalidDataException">
        /// thrown whenever no path is given
        /// </exception>
        public static void LoadFile()
        {
            string path = null;
            if (InputLength > 1) path = UserInputSplit[1];
            if (path == null) throw new InvalidDataException();
            if (File.Exists(path))
            {
                Activitys = InputLength > 2 ? new Activitys(path, UserInputSplit[2]) : new Activitys(path);
            }
            else
                Warnings.FileNotFound(path);
        }
        
        public static void LoadUserSettings()
        {
            // TODO make reader 
        }
    }
}