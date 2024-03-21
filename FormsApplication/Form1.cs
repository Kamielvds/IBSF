using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsApplication.PopUps;
using ProcessActivity;
using Scores;

namespace FormsApplication
{
    public partial class Form1 : Form
    {
        public string FilePath { get; set; }

        public Activities Activities;

        public AllScores AllScores { get => Activities.Scores; }

        public Form1()
        {
            InitializeComponent();
        }

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
            lsbScores.Items.Clear();
        }

        private void LoadScores()
        {
            var extension = FilePath.Split('.')[1];

            switch (extension)
            {
                case "txt":
                    var textReader = new Commands.DataProcessor.TextReader(FilePath);
                    Activities = new Activities(FilePath, "txt");
                    break;
                case "xml":
                    var xmlReader = new Commands.DataProcessor.XmlReader(FilePath);
                    Activities = new Activities(FilePath, "xml");
                    break;
            }
        }

        /// <summary>
        /// adds locations to the ListBox
        /// </summary>
        private void UpdateListbox()
        {
            
            lsbLocations.Items.Clear();
            foreach (var location in AllScores.Locations)
            {
                lsbLocations.Items.Add(location.Name);
            }

        }

       

        private void btnSettings_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            if (AllScores == null)
            {
                MessageBox.Show("There are no scores Loaded!");
            }
            else
            {
                var addScore = new AddScore(Activities);
               
                addScore.ActivitiesChanged += addScore_Returned;
                addScore.Show();

                Enabled = false;
            }

        }

        private void addScore_Returned(object sender, Activities activities)
        {
            Activities = activities;
            Enabled = true;
            UpdateListbox();

        }


        private void lsbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbScores.Items.Clear();
            for (var i = 0; i < AllScores.Locations[lsbLocations.SelectedIndex].Scores.Count; i++)
            {
                var score = AllScores.Locations[lsbLocations.SelectedIndex].Scores[i];
                lsbScores.Items.Add($"{i}: {score.Name}, {score.Date}, {score.Pace}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Activities.SaveFile();
        }

        private void lsbScores_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblScore.Text = ListScoreDetails(AllScores.Locations[lsbLocations.SelectedIndex].Scores[lsbScores.SelectedIndex]);
        }
        private static string ListScoreDetails(Score score)
        {
            var text = string.Empty;
            foreach (var item in score.AllObjects)
            {
                switch (item.Value)
                {
                    case null:
                        text +=($"\t \t {item.Key}: \n");
                        break;
                    case string _:
                        text +=(item.Key == "pace"
                            ? $"\t \t {item.Key}: {item.Value} km/h\n"
                            : $"\t \t {item.Key}: {item.Value}\n");
                        break;
                    default:
                        text +=($"\t \t splits:");
                        for (var i = 0; i < ((List<Score.Split>)item.Value).Count; i++)
                        {
                            var split = ((List<Score.Split>)item.Value)[i];
                            text +=($"\t \t \t split {i + 1}:\n");
                            text +=($"\t \t \t \t distance: {split.Distance}:\n");
                            text +=($"\t \t \t \t time: {split.Time + 1}:\n");
                        }

                        break;
                }
            }

            return text;
        }
    }
}