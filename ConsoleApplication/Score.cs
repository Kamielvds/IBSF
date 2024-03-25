using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApplication.Problems;
using Exceptions;
using ProcessActivity;
using Scores;
using Filtering;
//static 
using static ConsoleApplication.CustomMethods;
using TextWriter = Commands.DataProcessors.TextWriter;

namespace ConsoleApplication
{
   public static class ScoreCommand
   {
      private static Score LocalScore { get; set; }

      private static string[] UserInputSplit => Program.UserInputSplit;
      private static Activities Activities => Program.Activities;
      private static AllScores AllScores => Activities.Scores;

      /// <summary>
      /// Processes the given Score input
      /// </summary>
      public static void ProcessScoreCommand()
      {
         if (UserInputSplit.Length < 2) throw new NotEnoughArgumentsException();
         switch (UserInputSplit[1].ToLower())
         {
            case "list":
               ListScores();
               break;
            case "load":
            case "-l":
               LoadLocation();
               break;
            case "create":
            case "new":
               CreateScoreFile();
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
            case "backup":
               Activities.LoadBackup();
               break;
            case"compress":
               CompressScoreFile();
               break;
            default:
               throw new InvalidArgumentsException($"score:{UserInputSplit[1]}");
         }
      }

      private static void CompressScoreFile()
      {
         switch (Activities.Lang)
         {
            case "txt":
               var textWriter = new TextWriter(Activities.Path);
               textWriter.RewriteText(AllScores);
               break;
         }
      }

      private static void CreateScoreFile()
      {
         const string fileWarning = "The File already exists; do you wish to override it? (y/N)";

         Console.WriteLine("please give a lang:");
         var lang = Console.ReadLine();
         if (lang == null) throw new NullReferenceException();
         switch (lang.ToLower())
         {
            case "txt":
               if (File.Exists("Scores.txt"))
               {
                  Console.WriteLine(fileWarning);

                  switch (Console.ReadLine()?.ToLower())
                  {
                     case "y":
                     case "yes":
                        break;
                     case "no":
                     case "n":
                        // break out of the function
                        return;
                     default:
                        throw new InvalidArgumentsException();
                  }
               }

               var streamReader = new StreamWriter("Scores.txt");
               streamReader.Close();
               break;
            case "xml":
               if (File.Exists("Scores.xml"))
               {
                  Console.WriteLine(fileWarning);

                  switch (Console.ReadLine()?.ToLower())
                  {
                     case "y":
                     case "yes":
                        break;
                     case "no":
                     case "n":
                        // break out of the function
                        return;
                     default:
                        throw new InvalidArgumentsException();
                  }
               }

               var xmlReader = new StreamWriter("Scores.txt");
               xmlReader.Close();
               break;
            default:
               throw new InvalidArgumentsException();
         }

         if (!UserSettings.Debug) return;
         Console.ForegroundColor = ConsoleColor.Blue;
         Console.WriteLine("the file was successfully generated");
         Console.ForegroundColor = ConsoleColor.White;
      }

      /// <summary>
      /// edit a score
      /// </summary>
      /// <exception cref="EmptyScoreException">
      /// thrown when a score is not loaded into the localScore
      /// </exception>
      /// <exception cref="NotEnoughArgumentsException">
      /// thrown when not enough arguments are given
      /// </exception>
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
               LocalScore.Date = value;
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
                  ListSplitDetails(LocalScore.Splits);
                  EditSplit(Console.ReadLine());
               }

               EditSplit();
               break;
         }
      }

      /// <summary>
      /// edit the split
      /// </summary>
      /// <param name="split">
      /// the split which should be edited
      /// </param>
      /// <exception cref="NotEnoughArgumentsException"></exception>
      private static void EditSplit(string split = null)
      {
         int splitId;
         string splitItem;

         if (split == null)
         {
            // exc 
            if (UserInputSplit.Length < 5) throw new NotEnoughArgumentsException();

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
         if (UserInputSplit.Length < 6)
            value = UserInputSplit[5];
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

      /// <summary>
      /// how to compare the score
      /// </summary>
      /// <exception cref="NotEnoughArgumentsException">
      /// throw when not enough arguments are given
      /// </exception>
      private static void CompareScore()
      {
         if (UserInputSplit.Length < 3) throw new NotEnoughArgumentsException();
         switch (UserInputSplit[2])
         {
            case "in":
               CompareFrom();
               break;
            case "location":
               CompareLocations();
               break;
         }
      }

      /// <summary>
      /// displays the average pace of every score.
      /// </summary>
      private static void CompareLocations()
      {
         foreach (var location in AllScores.Locations)
         {
            if (location.Name != UserInputSplit[4]) continue;

            List<double> paces = location.Scores.Select(score => score.Pace).ToList();

            List<int> filteredList = Filters.SortAscendingIndex(paces);

            foreach (var t in filteredList)
            {
               Console.WriteLine($"Pace: {paces[t]} km/h");
            }

            return;
         }
      }

      /// <summary>
      /// gets from which type the score should be compared
      /// </summary>
      /// <exception cref="NotEnoughArgumentsException">
      /// throw when not enough arguments are given
      /// </exception>
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

      /// <summary>
      /// compare the location based on the length of the user input, when greater than five, compare inside of the location, else compare all the average paces inside of all locations
      /// </summary>
      private static void CompareLocation()
      {
         if (UserInputSplit.Length > 4)
            CompareInLocation();
         else
            throw new NotEnoughArgumentsException();
      }

      private static void CompareInLocation()
      {
         List<double> paces = new List<double>();
         foreach (var score in AllScores.Locations[AllScores.Find(UserInputSplit[4])].Scores)
         {
            paces.Add(score.Pace);
         }

         List<int> filteredPaces = Filters.SortAscendingIndex(paces);

         foreach (var paceIndex in filteredPaces)
         {
            Console.Write($"{paceIndex}: ");
            Console.Write(AllScores.Locations[AllScores.Find(UserInputSplit[4])].Scores[paceIndex].Name);
            Console.Write(AllScores.Locations[AllScores.Find(UserInputSplit[4])].Scores[paceIndex].Date);
            Console.Write(AllScores.Locations[AllScores.Find(UserInputSplit[4])].Scores[paceIndex].Pace);
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

         Console.WriteLine("How many splits?");
         for (var i = Convert.ToInt32(Console.ReadLine()); i > 0; i--)
         {
            Score.DistanceSeparator distanceSeparator;
            Score.TimeSeparator timeSeparator;
            
            
            Console.WriteLine("What time separator? (s/m/ms/h):");
            switch (Console.ReadLine()?.ToLower())
            {
               
               case "s":
               case "seconds":
                  timeSeparator = Score.TimeSeparator.Seconds;
                  break;
               case "m":
               case "minutes":
                  timeSeparator = Score.TimeSeparator.Minutes;
                  break;
               case "ms":
               case "milliseconds":
                  timeSeparator = Score.TimeSeparator.Milliseconds;
                  break;
               case "h":
               case "hours":
                  timeSeparator = Score.TimeSeparator.Hours;
                  break;
               default:
                  Console.ForegroundColor = ConsoleColor.Red;
                  Console.WriteLine("Invalid input found! meter has been used instead.");
                  Console.ForegroundColor = ConsoleColor.White;
                  timeSeparator = Score.TimeSeparator.Minutes;
                  break;
            }

            Console.WriteLine("What distance unit? (m/km):");
            switch (Console.ReadLine()?.ToLower())
            {

               case "m":
               case "meters":
                  distanceSeparator = Score.DistanceSeparator.Meters;
                  break;
               case "km":
               case "kilometers":
                  distanceSeparator = Score.DistanceSeparator.Kilometers;
                  break;
               default:
                  Console.ForegroundColor = ConsoleColor.Red;
                  Console.WriteLine("Invalid input found! meter has been used instead.");
                  Console.ForegroundColor = ConsoleColor.White;
                  distanceSeparator = Score.DistanceSeparator.Meters;
                  break;
            }

            Console.WriteLine("time:");
            int time = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("distance:");
            double distance =Convert.ToDouble(Console.ReadLine());
            
            Activities.CreateSplit(time, distance,timeSeparator,distanceSeparator);
         }

         // added last so if a convert issue happens it isn't all bad. (could be moved to try-catch branch :)) )
         try
         {
            if (gender != null)
               Activities.CreateScore(name, Convert.ToInt32(age), nationality, CheckBoolean(submitted),
                  date, Convert.ToChar(gender), note);
            else
               throw new NullReferenceException();
            Activities.CreateLocation(location);
         }
         catch (NullReferenceException)
         {
            Console.WriteLine("the gender was null.");
         }
         catch (Exception)
         {
            Console.WriteLine("the score couldn't be created because one of the parameters was wrong. " +
                              "Please check all the arguments and try again.");
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
         Console.WriteLine("id:"); // used as indicator
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
            if (location.Name != UserInputSplit[4]) continue;
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

      /// <summary>
      /// lists all the details about a score
      /// </summary>
      /// <param name="score">
      /// the score of which the details should be displayed
      /// </param>
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
                  Console.WriteLine(item.Key == "pace"
                     ? $"\t \t {item.Key}: {item.Value} km/h"
                     : $"\t \t {item.Key}: {item.Value}");
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

      /// <summary>
      /// lists all the info inside of the splits
      /// </summary>
      /// <param name="splits">
      /// the list of the splits to be listed
      /// </param>
      private static void ListSplitDetails(List<Score.Split> splits)
      {
         Console.WriteLine("id:");
         for (var i = 0; i < splits.Count; i++)
         {
            var split = splits[i];

            Console.WriteLine($"{i}: 0: time: {split.Time} 1: distance {split.Distance}");
         }
      }

      /// <summary>
      /// lists all the info inside of the split
      /// </summary>
      /// <param name="split">
      /// the split to be listed
      /// </param>
      private static void ListSplitDetails(Score.Split split)
      {
         Console.WriteLine("id:");
         Console.WriteLine($" 0: time: {split.Time} 1: distance {split.Distance}");
      }
   }
}