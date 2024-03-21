using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
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
        /// <summary>
        /// if this var changes to false, the program will terminate. check: 35:20, changed in 71:25
        /// </summary>
        private static bool _running = true;
        
        public static string UserInput;
        public static string Path;
        public static Activities Activities;
        public static string[] UserInputSplit => UserInput.Split(' ');

        public static int InputLength => UserInputSplit.Length;

        /// <summary>
        /// Entry point
        /// </summary>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public static void Main()
        {
            LoadUserSettings("userSettings.amlo");
            while (_running)
            {
                RequestUserInput();
            }
        }

        /// <summary>
        /// This is the Main cycle of the program, this will be fired everytime a operation completes.
        /// </summary>
        private static void RequestUserInput()
        {
            UserInput = Console.ReadLine();
            
            for (var i = 0; i < UserSettings.LinesAfterUser; i++)
                Console.WriteLine(Environment.NewLine);
            
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
                        Warnings.CommandNotFound(UserInputSplit[0]);
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.NotEnoughArguments();
            }
            catch (InvalidDataException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.InvalidDatatype();
            }
            catch (InvalidScoreException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.InvalidScore();
            }
            catch (EmptyLocationException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.EmptyLocation();
            }
            catch (NotEnoughArgumentsException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.NotEnoughArguments();
            }
            catch (InvalidArgumentsException s)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                string[] exceptionSplit = s.Message.Split(':');
                if (exceptionSplit.Length > 1)
                    Errors.InvalidParameter(exceptionSplit[0], exceptionSplit[1]);
            }
            catch (EmptyActivityException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.EmptyActivity();
            }
            catch (XmlException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (UserInputSplit[1].Split('.').Length > 1)
                    Errors.WrongFile("xml", UserInputSplit[1].Split('.')[1]);
                else Errors.WrongFile("none");
            }
            catch (ScoreNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.NotFound("score");
            }
            catch (LocationNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.NotFound("location");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.InvalidDatatype();
            }
            catch (EmptyScoreException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.EmptyScore();
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Errors.DependentError(ex.Message);
            }

            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < UserSettings.LinesBeforeUser; i++)
                Console.WriteLine(Environment.NewLine);
            
        }
    }
}