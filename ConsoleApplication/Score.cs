using System;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using ProcessActivity;
using Scores;
using Filtering;
//static 
using static ConsoleApplication.CustomMethods;

namespace ConsoleApplication
{
    public static class ScoreCommand
    {
        private static Score LocalScore { get; set; }

        private static string[] UserInputSplit => Program.UserInputSplit;
        private static Activitys Activitys => Program.Activitys;
        private static AllScores AllScores => Activitys.Scores;

        /// <summary>
        /// Processes the given Score input
        /// </summary>
        public static void ProcessScoreCommand()
        {
            if (Activitys == null) throw new EmptyActivityException();
            if (UserInputSplit.Length < 2) throw new NotEnoughArgumentsException();
            switch (UserInputSplit[1])
            {
                case "list":
                    ListScores();
                    break;
                case "load":
                case "-l":
                    LoadLocation();
                    break;
                case "compare":
                case "-c":
                    CompareScore();
                    break;
                case "add":
                case "-a":
                    AddScore();
                    break;
                case "edit":
                case "-e":
                    EditScore();
                    break;
                default:
                    throw new InvalidArgumentsException($"score:{UserInputSplit[1]}");
            }
        }

        private static void EditScore()
        {
            if (LocalScore == null) throw new EmptyScoreException();
            if (UserInputSplit.Length < 3) throw new NotEnoughArgumentsException();

            string value = UserInputSplit[2];
            
            switch (UserInputSplit[2].ToLower())
            {
                case "name":
                    LocalScore.Name = value;
                    break;
                case "age":
                    LocalScore.Age = Convert.ToInt32(value);
                    break;
                case "nationality":
                    LocalScore.Nationality = value;
                    break;
                case "date":
                    LocalScore.Date = Convert.ToDateTime(value);
                    break;
                case "gender":
                    LocalScore.Gender = Convert.ToChar(value);
                    break;
                case "note":
                    LocalScore.Note = value;
                    break;
                case "submitted":
                    LocalScore.Submitted = CheckBoolean(value);
                    break;
                case "splits":
                    if (UserInputSplit.Length == 3)
                    {
                        ListSplitsDetails(LocalScore.Splits);
                        EditSplit(Console.ReadLine());
                    }
                    EditSplit();
                    break;
            }
        }

        private static void EditSplit(string split = null)
        {
            int splitId; 
            string splitItem;
            
            if (split == null)
            {
                // exc 
                if (UserInputSplit.Length < 5)
                    throw new NotEnoughArgumentsException();
                
                splitId = Convert.ToInt32(UserInputSplit[3]);
                // between 0-1, otherwise IOoR Ex
                splitItem = UserInputSplit[4];
            }
            else
            {
                splitId = Convert.ToInt32(split.Split(' ')[0]);
                splitItem = split.Split(' ')[1];
            }
            
           

            string value;
            if (UserInputSplit.Length < 6) value = UserInputSplit[5];
            else
            {
                Console.WriteLine("give value: ");
                value = Console.ReadLine();
            }
            
            // saved inst by using string 
            /*
             * 0 -> time
             * 1 -> distance
             */
            switch (splitItem)
            {
                case "0":
                    LocalScore.Splits[splitId].Time = Convert.ToInt64(value);
                    break;
                case "1":
                    LocalScore.Splits[splitId].Distance = Convert.ToDouble(value);
                    break;
            }
            
        }

        /// <summary>
        /// used for loading in the local score.
        /// </summary>
        private static void LoadLocation()
        {
            if (UserInputSplit.Length > 3)
                LoadScore();
            else
            {
                Console.WriteLine("id:");
                for (var i = 0; i < AllScores.Locations.Count; i++)
                {
                    var location = AllScores.Locations[i];
                    Console.WriteLine($"{i}: {location.Name}");
                }

                LoadScore(Console.ReadLine());
            }
        }

        /// <summary>
        /// used to load in the score
        /// </summary>
        /// <param name="location">
        /// the location by default is null, if remained null will use the UserInputSplit.
        /// </param>
        private static void LoadScore(string location = null)
        {
            if (location == null) location = UserInputSplit[2];
            if (!AllScores.LocationExists(location)) throw new LocationNotFoundException(location);

            int locationIndex = AllScores.Find(location);

            if (UserInputSplit.Length > 4)
                // :( 
                LocalScore = AllScores.Locations[locationIndex].Scores[Convert.ToInt32(UserInputSplit[3])];
            else
            {
                foreach (var score in AllScores.Locations[locationIndex].Scores)
                {
                    ListScoreDetails(score);
                }

                LocalScore = AllScores.Locations[locationIndex].Scores[Convert.ToInt32(Console.ReadLine())];
            }
        }

        private static void CompareScore()
        {
            if (UserInputSplit.Length < 3) throw new NotEnoughArgumentsException();
            switch (UserInputSplit[2])
            {
                case "in":
                    CompareFrom();
                    break;
            }
        }

        private static void CompareFrom()
        {
            if (UserInputSplit.Length < 4) throw new NotEnoughArgumentsException();
            switch (UserInputSplit[3])
            {
                case "location":
                    CompareLocation();
                    break;
            }
        }

        private static void CompareLocation()
        {
            if (UserInputSplit.Length < 5)
                CompareInLocation();
            else
                foreach (var location in AllScores.Locations)
                {
                    if (location.Name  != UserInputSplit[4]) continue;

                    List<double> paces = location.Scores.Select(score => score.Pace).ToList();

                    List<int> filtredlist = Filters.SortAscendingIndex(paces);

                    foreach (var t in filtredlist)
                    {
                        Console.WriteLine($"Pace: {paces[t]} km/h");
                    }

                    return;
                }
        }

        private static void CompareInLocation()
        {
            // todo
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

            // added last so if a convert issue happens it isn't all bad. (could be moved to try-catch branch :)) )
            try
            {
                Activitys.CreateSplit(times, distances);
                if (gender != null)
                    Activitys.CreateScore(name, Convert.ToInt32(age), nationality, CheckBoolean(submitted),
                        Convert.ToDateTime(date), Convert.ToChar(gender), note);
                else
                    throw new NullReferenceException();
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
        /// <exception cref="NotEnoughArgumentsException">
        /// thrown when there aren't enough arguments
        /// </exception>
        private static void ListScores()
        {
            if (UserInputSplit.Length < 3) throw new NotEnoughArgumentsException();
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
            if (UserInputSplit.Length < 4) throw new NotEnoughArgumentsException();
            switch (UserInputSplit[3])
            {
                case "location":
                    ListLocation();
                    break;
            }
        }

        /// <summary>
        /// finds location and lists all the scores inside 
        /// </summary>
        /// <exception cref="NotEnoughArgumentsException">
        /// throw when not enough arguments are given.
        /// </exception>
        /// <exception cref="LocationNotFoundException">
        /// throw when location is not found
        /// </exception>
        private static void ListLocation()
        {
            if (UserInputSplit.Length < 5) throw new NotEnoughArgumentsException();

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

            throw new LocationNotFoundException();
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

                    ListScoreDetails(score);
                }
            }
        }

        private static void ListScoreDetails(Score score)
        {
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

        private static void ListSplitsDetails(List<Score.Split> splits)
        {
            Console.WriteLine("id:");
            for (var i = 0; i < splits.Count; i++)
            {
                var split = splits[i];
                
                Console.WriteLine($"{i}: 0: time: {split.Time} 1: distance {split.Distance}");
            }
        }
    }
}