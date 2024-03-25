using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using FormsApplication;
using ProcessActivity;
using Scores;

namespace FormsApplication.PopUps
{
    public partial class AddScore : Form
    {

        List<Score.Split> _splits = new List<Score.Split>();

        private List<Location> Locations { get => AllScores.Locations; }
        private AllScores AllScores { get => Activities.Scores; }
        private Activities Activities { get; set; }

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

        private void btnAddSplit_Click(object sender, System.EventArgs e)
        {
            lsbSplits.Items.Add(lsbSplits.Items.Count + 1);
            _splits.Add(new Score.Split());
        }

        private void lsbSplits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            grbSplit.Visible = true;

            //TODO save instead of button save
            
            txtDistance.Text = _splits[lsbSplits.SelectedIndex].Distance.ToString(CultureInfo.InvariantCulture);
            txtTime.Text = _splits[lsbSplits.SelectedIndex].Time.ToString();

            if (_splits[lsbSplits.SelectedIndex].DistanceUnit != null)
                cboDistance.Text = _splits[lsbSplits.SelectedIndex].DistanceUnit.ToString();
            if (_splits[lsbSplits.SelectedIndex].TimeUnit != null)
                cboTime.Text = _splits[lsbSplits.SelectedIndex].TimeUnit.ToString();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _splits[lsbSplits.SelectedIndex].Distance = Convert.ToDouble(txtDistance.Text);
            _splits[lsbSplits.SelectedIndex].Time = Convert.ToInt64(txtTime.Text);
            _splits[lsbSplits.SelectedIndex].TimeUnit = cboTime.Text;
            _splits[lsbSplits.SelectedIndex].DistanceUnit = cboDistance.Text;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // TODO null check & save split

            Activities.CreateScore(txtName.Text, Convert.ToInt32(txtAge.Text), txtNat.Text, chbSubmitted.Checked, dtpDateTime.Text, Convert.ToChar(cboGender.Text), txtNote.Text, _splits);
            Activities.CreateLocation(cboLocations.Text);
            ActivitiesChanged?.Invoke(this, Activities);
            Close();
        }
    }
}