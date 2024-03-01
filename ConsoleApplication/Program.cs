using System;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;
using Exceptions;
// static Classes inside project
using static ConsoleApplication.Settings;
using static ConsoleApplication.Loading;
using static ConsoleApplication.Saving;
using static ConsoleApplication.ScoreCommand;

namespace ConsoleApplication
{
    internal abstract class Program
    {
        private static bool _running = true;
        public static string UserInput;
        public static string Path;
        public static Activitys Activitys;
        public static string[] UserInputSplit => UserInput.Split(' ');

        public static int InputLength => UserInputSplit.Length;

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            LoadUserSettings("userSettings.amlo");
            while (_running)
            {
                for (var i = 0; i < UserSettings.LinesBeforeUser; i++)
                    Console.WriteLine(Environment.NewLine);
                RequestUserInput();
                for (var i = 0; i < UserSettings.LinesAfterUser; i++)
                    Console.WriteLine(Environment.NewLine);
            }
        }

        /// <summary>
        /// This is the Main cylce of the program, this will be fired everytime a operation completes.
        /// </summary>
        private static void RequestUserInput()
        {
            UserInput = Console.ReadLine();
            if (UserInput == null) return;
            try
            {
                switch (UserInputSplit[0].ToLower())
                {
                    case "load":
                        LoadFile();
                        break;
                    case "save":
                        SaveFile();
                        break;
                    case "setting":
                    case "--s":
                        ProcessSettingCommand();
                        break;
                    case "score":
                        ProcessScoreCommand();
                        break;
                    case "quit":
                    case "q":
                        _running = false;
                        break;
                    default:
                        Warnings.CommandNotFound();
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Errors.NotEnoughArguments();
            }
            catch (InvalidDataException)
            {
                Errors.InvalidDatatype();
            }
            catch (InvalidScoreException)
            {
                Errors.InvalidScore();
            }
            catch (EmptyLocationException)
            {
                Errors.EmptyLocation();
            }
            catch (NotEnoughArguments)
            {
                Errors.NotEnoughArguments();
            }
            catch (InvalidArguments s)
            {
                string[] exceptionSplit = s.Message.Split(':');
                Errors.InvalidParameter(exceptionSplit[0], exceptionSplit[1]);
            }
            catch (EmptyActivityException)
            {
                Errors.EmptyActivity();
            }
        }
    }
}