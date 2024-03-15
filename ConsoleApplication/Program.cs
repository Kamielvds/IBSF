﻿using System;
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
            catch (NotEnoughArgumentsException)
            {
                Errors.NotEnoughArguments();
            }
            catch (InvalidArgumentsException s)
            {
                string[] exceptionSplit = s.Message.Split(':');
                Errors.InvalidParameter(exceptionSplit[0], exceptionSplit[1]);
            }
            catch (EmptyActivityException)
            {
                Errors.EmptyActivity();
            }
            catch (XmlException)
            {
                if (UserInputSplit[1].Split('.').Length > 1)
                    Errors.WrongFile("xml", UserInputSplit[1].Split('.')[1]);
                Errors.WrongFile("xml");
            }
            catch (ScoreNotFoundException)
            {
                Errors.NotFound("score");
            }
            catch (LocationNotFoundException)
            {
                Errors.NotFound("location");
            }
            catch (FormatException)
            {
                Errors.InvalidDatatype();
            }
            catch (EmptyScoreException)
            {
                Errors.EmptyScore();
            }
            
            for (var i = 0; i < UserSettings.LinesBeforeUser; i++)
                Console.WriteLine(Environment.NewLine);
        }
    }
}