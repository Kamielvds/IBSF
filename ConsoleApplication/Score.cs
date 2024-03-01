using System;
using System.Collections.Generic;
using Exceptions;
using ProcessActivity;
using Scores;

namespace ConsoleApplication
{
    public class ScoreCommand
    {
        private static string[] UserInputSplit => Program.UserInputSplit;
        private static Activitys Activitys => Program.Activitys;
        private static AllScores AllScores => Activitys.Scores;
        
        /// <summary>
        /// Processes the given Score input
        /// </summary>
        public static void ProcessScoreCommand()
        {
            if (Activitys == null) throw new EmptyActivityException();
            if (UserInputSplit.Length < 2) throw new NotEnoughArguments();
            switch (UserInputSplit[1])
            {
                case "list":
                case "-l":
                    ListScores();
                    break;
                default:
                    throw new InvalidArguments($"score:{UserInputSplit[1]}");
            }
        }

        /// <summary>
        /// decide which scores should be listed
        /// </summary>
        /// <exception cref="NotEnoughArguments">
        /// thrown when there aren't enough arguments
        /// </exception>
        private static void ListScores()
        {
            if (UserInputSplit.Length < 3) throw new NotEnoughArguments();
            switch (UserInputSplit[2])
            {
                case "*":
                    ListAllScores();
                    break;
                case "in":
                    ListFrom();
                    break;
            }
        }

        /// <summary>
        /// Finds where the scores should be red from.
        /// </summary>
        private static void ListFrom()
        {
            // TODO
        }

        /// <summary>
        /// list all scores from Activity's class
        /// </summary>
        private static void ListAllScores()
        {
            
            foreach (var location in AllScores.Locations)
            {
                Console.WriteLine($"Location: {location.Name}:");
                for (var j = 0; j < location.Scores.Count; j++)
                {
                    var score = location.Scores[j];
                    Console.WriteLine($"\t Activity {j + 1}:");
                    foreach (var item in score.AllObjects)
                    {
                        switch (item.Value)
                        {
                            case null:
                                Console.WriteLine($"\t \t {item.Key}:");
                                break;
                            case string _:
                                Console.WriteLine($"\t \t {item.Key}: {item.Value}");
                                break;
                            default:
                                Console.WriteLine($"\t \t splits:");
                                for (var i = 0; i < ((List<Score.Split>)item.Value).Count; i++)
                                {
                                    var split = ((List<Score.Split>)item.Value)[i];
                                    Console.WriteLine($"\t \t \t split {i + 1}:");
                                    Console.WriteLine($"\t \t \t \t distance: {split.Distance}:");
                                    Console.WriteLine($"\t \t \t \t time: {split.Time + 1}:");
                                }

                                break;
                        }
                    }
                }
            }
        }
    }
}