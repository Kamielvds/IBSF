using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ProcessActivity;
using Scores;

namespace FormsApplication.PopUps
{
    public partial class AddScore : Form
    {
        private readonly List<Score.Split> _splits = new List<Score.Split>();

        private List<Location> Locations => AllScores.Locations;
        private AllScores AllScores => Activities.Scores;
        private Activities Activities { get; set; }

        private int _previousIndex = -1;

        public event EventHandler<Activities> ActivitiesChanged;

        public AddScore(Activities activities)
        {
            InitializeComponent();

            Activities = activities;

            // fill combobox
            foreach (var location in Locations)
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

        private void btnAddSplit_Click(object sender, EventArgs e)
        {
            lsbSplits.Items.Add(lsbSplits.Items.Count + 1);
            _splits.Add(new Score.Split());
        }

        private void lsbSplits_SelectedIndexChanged(object sender, EventArgs e)
        {
            grbSplit.Visible = true;

            if (_previousIndex != -1)
                UpdateSplit(_previousIndex);
            
            // update previous index since it will no longer be used until next index change.
            _previousIndex = lsbSplits.SelectedIndex;
            
            txtDistance.Text = _splits[lsbSplits.SelectedIndex].Distance.ToString(CultureInfo.InvariantCulture);
            txtTime.Text = _splits[lsbSplits.SelectedIndex].Time.ToString();

            if (_splits[lsbSplits.SelectedIndex].DistanceUnit != null)
                cboDistance.Text = _splits[lsbSplits.SelectedIndex].DistanceUnit;
            if (_splits[lsbSplits.SelectedIndex].TimeUnit != null)
                cboTime.Text = _splits[lsbSplits.SelectedIndex].TimeUnit;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSplit(lsbSplits.SelectedIndex);
        }
        private void UpdateSplit(int index)
        {
            _splits[index].Distance = Convert.ToDouble(txtDistance.Text);
            _splits[index].Time = Convert.ToInt64(txtTime.Text);
            _splits[index].TimeUnit = cboTime.Text;
            _splits[index].DistanceUnit = cboDistance.Text;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            UpdateSplit(lsbSplits.SelectedIndex);

            Activities.CreateScore(txtName.Text, Convert.ToInt32(txtAge.Text), txtNat.Text, chbSubmitted.Checked, dtpDateTime.Text, Convert.ToChar(cboGender.Text), txtNote.Text, _splits);
            Activities.CreateLocation(cboLocations.Text);
            ActivitiesChanged?.Invoke(this, Activities);
            Close();
        }

        private void AddScore_Leave(object sender, EventArgs e)
        {
            ActivitiesChanged?.Invoke(this, Activities);
            Close();
        }
    }
}