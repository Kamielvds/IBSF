using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FormsApplication;
using ProcessActivity;
using Scores;

namespace FormsApplication.PopUps
{
    public partial class AddScore : Form
    {

        List<Score.Split> splits = new List<Score.Split>();

        private List<Location> locations { get => allScores.Locations; }
        private AllScores allScores { get => Activities.Scores; }
        private Activities Activities { get; set; }

        public event EventHandler<Activities> ActivitiesChanged;

        public AddScore(Activities activities)
        {
            InitializeComponent();

            Activities = activities;

            // fill combobox
            foreach (var location in locations)
            {
                cboLocations.Items.Add(location.Name);
            }

            grbSplit.Visible = false;

            cboGender.Items.Add('M');
            cboGender.Items.Add('F');

            cboDistance.Items.Add(Score.DistanceSeparator.Kilometers.ToString());
            cboDistance.Items.Add(Score.DistanceSeparator.Meters.ToString());

            cboTime.Items.Add(Score.TimeSeparator.Hours.ToString());
            cboTime.Items.Add(Score.TimeSeparator.Minutes.ToString());
            cboTime.Items.Add(Score.TimeSeparator.Seconds.ToString());
            cboTime.Items.Add(Score.TimeSeparator.Milliseconds.ToString());


        }

        private void btnAddSplit_Click(object sender, System.EventArgs e)
        {
            lsbSplits.Items.Add(lsbSplits.Items.Count + 1);
            splits.Add(new Score.Split());
            btnSaveScore.Visible = true;
        }

        private void lsbSplits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            grbSplit.Visible = true;

            txtDistance.Text = splits[lsbSplits.SelectedIndex].Distance.ToString();
            txtTime.Text = splits[lsbSplits.SelectedIndex].Time.ToString();

            if (splits[lsbSplits.SelectedIndex].DistanceUnit != null)
                cboDistance.Text = splits[lsbSplits.SelectedIndex].DistanceUnit.ToString();
            if (splits[lsbSplits.SelectedIndex].TimeUnit != null)
                cboTime.Text = splits[lsbSplits.SelectedIndex].TimeUnit.ToString();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            splits[lsbSplits.SelectedIndex].Distance = Convert.ToDouble(txtDistance.Text);
            splits[lsbSplits.SelectedIndex].Time = Convert.ToInt64(txtTime.Text);
            splits[lsbSplits.SelectedIndex].TimeUnit = cboTime.Text;
            splits[lsbSplits.SelectedIndex].DistanceUnit = cboDistance.Text;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // TODO null check

            Activities.CreateScore(txtName.Text, Convert.ToInt32(txtAge.Text), txtNat.Text, chbSubmitted.Checked, dtpDateTime.Text, Convert.ToChar(cboGender.Text), txtNote.Text, splits);
            Activities.CreateLocation(cboLocations.Text);
            ActivitiesChanged?.Invoke(this, Activities);
            Close();
        }
    }
}