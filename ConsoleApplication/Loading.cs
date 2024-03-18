using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    public static class Loading
    {
        private static int InputLength => Program.InputLength;
        private static string[] UserInputSplit => Program.UserInputSplit;

        private static string Path
        {
            get => Program.Path;
            set => Program.Path = value;
        }

        private static Activities Activities
        {
            get => Program.Activities;
            set => Program.Activities = value;
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
            {
                Activities = InputLength > 2 ? new Activities(Path, UserInputSplit[2]) : new Activities(Path);
            }
            else
                Warnings.FileNotFound(Path);
            
            
        }

        /// <summary>
        /// load the settings from the user
        /// </summary>
        /// <param name="path">
        /// the path of the settings file
        /// </param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
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
                        if (lnSplit[1] == "1")
                            UserSettings.ShowWarnings = true;
                        else
                            Warnings.SettingValueNotValid(lnSplit[1], lnSplit[0]);
                        break;
                    case "showerrors":
                        if (lnSplit[1] == "0") UserSettings.ShowErrors = false;
                        if (lnSplit[1] == "1")
                            UserSettings.ShowErrors = true;
                        else
                            Warnings.SettingValueNotValid(lnSplit[1], lnSplit[0]);
                        break;
                    case "linesbeforeuser":
                        UserSettings.LinesBeforeUser = Convert.ToInt32(lnSplit[1]);
                        break;
                    case "linesafteruser":
                        UserSettings.LinesAfterUser = Convert.ToInt32(lnSplit[1]);
                        break;
                    case "default":
                        Warnings.SettingNotValid(lnSplit[0]);
                        break;
                }
            }
        }
    }
}