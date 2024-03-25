using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FormsApplication.PopUps;
using ProcessActivity;
using Scores;
using TextWriter = Commands.DataProcessors.TextWriter;

namespace FormsApplication
{
   public partial class Form1 : Form
   {
      private string FilePath { get; set; }

      private Activities _activities;

      private AllScores AllScores => _activities.Scores;

      public Form1()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Selecting a file and loading the filePath locally
      /// </summary>
      /// <param name="sender">
      /// the button of which has used to call this method
      /// </param>
      /// <param name="e">
      /// the event arguments
      /// </param>
      private void button1_Click(object sender, EventArgs e)
      {
         var openFileDialog = new OpenFileDialog();

         openFileDialog.Title = @"Select a File";
         openFileDialog.Filter = @"XML Files (*.xml) or Text Files (*.txt)|*.xml; *.txt|All Files (*.*)|*.*";
         openFileDialog.FilterIndex = 1;

         DialogResult result = openFileDialog.ShowDialog();

         if (result != DialogResult.OK) return;
         FilePath = openFileDialog.FileName;

         LoadScores();
         UpdateListbox();
      }

      /// <summary>
      /// load all scores based on the lang type
      /// </summary>
      private void LoadScores()
      {
         var pathSplit = FilePath.Split('\\');
         string extension = pathSplit[pathSplit.Length -1].Split('.')[1];
         
         switch (extension)
         {
            case "txt":
               _activities = new Activities(FilePath, "txt");
               break;
            case "xml":
               _activities = new Activities(FilePath, "xml");
               break;
         }
      }

      /// <summary>
      /// adds locations to the ListBox
      /// </summary>
      private void UpdateListbox()
      {
         lsbScores.Items.Clear();
         lsbLocations.Items.Clear();
         foreach (var location in AllScores.Locations)
         {
            lsbLocations.Items.Add(location.Name);
         }
      }

      /// <summary>
      /// open the settings window
      /// </summary>
      /// <param name="sender">
      ///  the button that's used to call
      /// </param>
      /// <param name="e">
      /// the event args
      /// </param>
      private void btnSettings_Click(object sender, EventArgs e)
      {
         var settingsWindow = new Settings();
         settingsWindow.Show();
      }

      /// <summary>
      /// open a new add score window
      /// </summary>
      /// <param name="sender">
      /// the button that's used to call
      /// </param>
      /// <param name="e">
      /// the event args
      /// </param>
      private void btnAddScore_Click(object sender, EventArgs e)
      {
         if (AllScores == null)
         {
            MessageBox.Show(@"There are no scores Loaded!");
         }
         else
         {
            var addScore = new AddScore(_activities);

            addScore.ActivitiesChanged += addScore_Returned;
            addScore.Show();

            Enabled = false;
         }
      }

      /// <summary>
      /// enable this and reload the listbox
      /// </summary>
      /// <param name="sender">
      /// the form used to call this function
      /// </param>
      /// <param name="activities">
      /// the returned activities
      /// </param>
      private void addScore_Returned(object sender, Activities activities)
      {
         _activities = activities;
         Enabled = true;
         UpdateListbox();
      }

      /// <summary>
      /// reloading all scores listbox
      /// </summary>
      /// <param name="sender">
      /// the object that calls this function
      /// </param>
      /// <param name="e">
      /// the event args
      /// </param>
      private void lsbLocations_SelectedIndexChanged(object sender, EventArgs e)
      {
         lsbScores.Items.Clear();
         if (lsbLocations.SelectedIndex == -1) {
            lsbScores.Items.Clear(); // clears all the scores, because nothing is selected
            return;
         }
         for (var i = 0; i < AllScores.Locations[lsbLocations.SelectedIndex].Scores.Count; i++)
         {
            var score = AllScores.Locations[lsbLocations.SelectedIndex].Scores[i];
            lsbScores.Items.Add($"{i}: {score.Name}, {score.Date}, {score.Pace}");
         }
      }

      /// <summary>
      /// Saving the file
      /// </summary>
      /// <param name="sender">
      /// the button which called this function
      /// </param>
      /// <param name="e">
      /// the event args
      /// </param>
      private void btnSave_Click(object sender, EventArgs e)
      {
         _activities.SaveFile();
      }

      /// <summary>
      /// Load the Score
      /// </summary>
      /// <param name="sender">
      ///
      ///  the object that calls this function</param>
      /// <param name="e">
      /// the event args
      /// </param>
      private void lsbScores_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (lsbScores.SelectedIndex == -1)
         {
            lblScore.Text = string.Empty;
            return;
         } 
         lblScore.Text =
            ListScoreDetails(AllScores.Locations[lsbLocations.SelectedIndex].Scores[lsbScores.SelectedIndex]);
      }

      /// <summary>
      /// Lists all the details
      /// </summary>
      /// <param name="score">
      /// the score of which the details should be displayed
      /// </param>
      /// <returns>
      /// the string with details
      /// </returns>
      private static string ListScoreDetails(Score score)
      {
         var text = string.Empty;
         foreach (var item in score.AllObjects)
         {
            switch (item.Value)
            {
               case null:
                  text += ($"\t \t {item.Key}: \n");
                  break;
               case string _:
                  text += (item.Key == "pace"
                     ? $"\t \t {item.Key}: {item.Value} km/h\n"
                     : $"\t \t {item.Key}: {item.Value}\n");
                  break;
               default:
                  text += ($"\t \t splits: \n");
                  for (var i = 0; i < ((List<Score.Split>)item.Value).Count; i++)
                  {
                     var split = ((List<Score.Split>)item.Value)[i];
                     text += ($"\t \t \t split {i + 1}:\n");
                     text += ($"\t \t \t \t distance: {split.Distance}:\n");
                     text += ($"\t \t \t \t time: {split.Time}:\n");
                     text += ($"\t \t \t \t pace: {split.Pace}:\n");
                  }

                  break;
            }
         }

         return text;
      }

      /// <summary>
      /// compresses the Score file and prints the saved bytes
      /// </summary>
      /// <param name="sender">
      /// the object from which it's sent
      /// </param>
      /// <param name="e">
      /// the eventArgs
      /// </param>
      private void button2_Click(object sender, EventArgs e)
      {
         var textWriter = new TextWriter(FilePath);
         string compressedText = textWriter.CompressText(AllScores);
         var fileInfoOld = new FileInfo(FilePath);
         var fileInfoNew = new FileInfo(compressedText);
         lblScore.Text = $@"The file was compressed successfully; {fileInfoOld.Length - fileInfoNew.Length} bytes saved!";
      }
   }
}