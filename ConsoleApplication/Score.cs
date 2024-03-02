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
                case "add":
                    AddScore();
                    break;
                default:
                    throw new InvalidArguments($"score:{UserInputSplit[1]}");
            }
        }

        /// <summary>
        /// Add a score to the Activity's local score system, !!! not saved to file !!!
        /// </summary>
        private static void AddScore()
        {
            Console.WriteLine("location name:");
            var location = Console.ReadLine();
            
            // score
            Console.WriteLine("name:");
            var name = Console.ReadLine();
            Console.WriteLine("age:");
            var age = Console.ReadLine();
            Console.WriteLine("note:");
            var note = Console.ReadLine();
            Console.WriteLine("gender:");
            var gender = Console.ReadLine();
            Console.WriteLine("nationality:");
            var nationality = Console.ReadLine();
            Console.WriteLine("date:");
            var date = Console.ReadLine();
            Console.WriteLine("submitted:");
            var submitted = Console.ReadLine();

            var times       = new List<long>();
            var distances   = new List<double>();
            
            Console.WriteLine("How many splits?");
            for (int i = Convert.ToInt32(Console.ReadLine()); i > 0 ; i--)
            {
                
                Console.WriteLine("time:");
                times.Add(Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine("distance:");
                distances.Add(Convert.ToDouble(Console.ReadLine()));
            }
            
            
            
            // added last so if a convert issue happens it isn't all bad.
            try
            {
                Activitys.CreateSplit(times, distances);
                if (gender != null)
                    Activitys.CreateScore(name, Convert.ToInt32(age), nationality, Convert.ToBoolean(submitted),
                        Convert.ToDateTime(date), Convert.ToChar(gender), note);
                else throw new NullReferenceException();
                Activitys.CreateLocation(location);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("the gender was null.");
            }
            catch (Exception)
            {
                Console.WriteLine("the score coudn't be created because one of the parameters was wrong. " +
                                  "Please check all the arguments and try again.");
                throw;
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
                case "location":
                    ListAllLocations();
                    break;
                case "in":
                    ListFrom();
                    break;
            }
        }

        /// <summary>
        /// List all the different scores.
        /// </summary>
        private static void ListAllLocations()
        {
            Console.WriteLine("id:");   // used as indicator
            for (var i = 0; i < AllScores.Locations.Count; i++)
            {
                var location = AllScores.Locations[i];
                Console.WriteLine($" {i}: {location.Name}");
            }
        }

        /// <summary>
        /// Finds where the scores should be red from.
        /// </summary>
        private static void ListFrom()
        {
            if (UserInputSplit.Length < 4) throw new NotEnoughArguments();
            switch (UserInputSplit[3])
            {
                case "location":
                    ListLocation();
                    break;
            }
        }

        private static void ListLocation()
        {
            if (UserInputSplit.Length < 5) throw new NotEnoughArguments();
            try
            {
                foreach (var location in AllScores.Locations)
                {
                    if (location.Name  != UserInputSplit[4]) continue;
                    for (var j = 0; j < location.Scores.Count; j++)
                    {
                        var score = location.Scores[j];
                        Console.WriteLine($"Activity {j + 1}:");
                        foreach (var item in score.AllObjects)
                        {
                            switch (item.Value)
                            {
                                case null:
                                    Console.WriteLine($"\t {item.Key}:");
                                    break;
                                case string _:
                                    Console.WriteLine($"\t {item.Key}: {item.Value}");
                                    break;
                                default:
                                    Console.WriteLine($"\t splits:");
                                    for (var i = 0; i < ((List<Score.Split>)item.Value).Count; i++)
                                    {
                                        var split = ((List<Score.Split>)item.Value)[i];
                                        Console.WriteLine($"\t \t split {i + 1}:");
                                        Console.WriteLine($"\t \t \t distance: {split.Distance}:");
                                        Console.WriteLine($"\t \t \t time: {split.Time + 1}:");
                                    }

                                    break;
                            }
                        }
                    }
                    return;
                }
                Console.WriteLine("Score was not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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