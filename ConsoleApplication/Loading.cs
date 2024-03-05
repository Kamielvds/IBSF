using System;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    public static class Loading
    {
        private static int InputLength => Program.InputLength;
        private static string [] UserInputSplit => Program.UserInputSplit;

        private static string Path
        {
            get => Program.Path;
            set => Program.Path = value;
        }

        private static Activitys Activitys
        {
            get => Program.Activitys;
            set => Program.Activitys = value;
        }

        /// <summary>
        /// Loading the sports file, and saving it to _activity's
        /// </summary>
        /// <exception cref="InvalidDataException">
        /// thrown whenever no path is given
        /// </exception>
        public static void LoadFile()
        {
            if (InputLength > 1) Path = UserInputSplit[1];

            if (Path == null) throw new InvalidDataException();

            if (File.Exists(Path))
                Activitys = InputLength > 2 ? new Activitys(Path, UserInputSplit[2]) : new Activitys(Path);
            else
                Warnings.FileNotFound(Path);
        }

       public static void LoadUserSettings(string path)
        {
            var sr = new StreamReader(path);
            string ln;
            while ((ln = sr.ReadLine()) != null)
            {
                var lnSplit = ln.Split(' ');
                switch (lnSplit[0].ToLower())
                {
                    case "showwarnings":
                        if (lnSplit[1] == "0") UserSettings.ShowWarnings = false;
                        if (lnSplit[1] == "1") UserSettings.ShowWarnings = true; 
                        else Warnings.SettingValueNotValid(lnSplit[1], lnSplit[0]);
                        break;
                    case "showerrors":
                        if (lnSplit[1] == "0") UserSettings.ShowErrors = false;
                        if (lnSplit[1] == "1") UserSettings.ShowErrors = true; 
                        else Warnings.SettingValueNotValid(lnSplit[1], lnSplit[0]);
                        break;
                    case "linesbeforeuser":
                    case "linesafteruser":
                        try
                        {
                            if (lnSplit[0] == "linesbeforeuser")
                                UserSettings.LinesBeforeUser = Convert.ToInt32(lnSplit[1]);
                            else
                                UserSettings.LinesAfterUser = Convert.ToInt32(lnSplit[1]);
                        }
                        catch (Exception)
                        {
                            if (lnSplit.Length > 1)
                                Warnings.SettingValueNotValid(lnSplit[1], lnSplit[0]);
                            else
                                Errors.InvalidParameter(lnSplit[0]);
                        }
                        break; 
                    case "default":
                        Warnings.SettingNotValid(lnSplit[0]);
                        break;
                }
            }
        }
    }
}