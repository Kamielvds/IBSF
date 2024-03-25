using System.ComponentModel;

namespace FormsApplication.PopUps
{
   partial class AddScore
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }

         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.cboLocations = new System.Windows.Forms.ComboBox();
         this.txtName = new System.Windows.Forms.TextBox();
         this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
         this.cboGender = new System.Windows.Forms.ComboBox();
         this.txtAge = new System.Windows.Forms.TextBox();
         this.chbSubmitted = new System.Windows.Forms.CheckBox();
         this.lsbSplits = new System.Windows.Forms.ListBox();
         this.btnAddSplit = new System.Windows.Forms.Button();
         this.txtDistance = new System.Windows.Forms.TextBox();
         this.txtTime = new System.Windows.Forms.TextBox();
         this.label8 = new System.Windows.Forms.Label();
         this.label9 = new System.Windows.Forms.Label();
         this.grbSplit = new System.Windows.Forms.GroupBox();
         this.button1 = new System.Windows.Forms.Button();
         this.grbUnits = new System.Windows.Forms.GroupBox();
         this.cboDistance = new System.Windows.Forms.ComboBox();
         this.cboTime = new System.Windows.Forms.ComboBox();
         this.txtNat = new System.Windows.Forms.TextBox();
         this.label10 = new System.Windows.Forms.Label();
         this.txtNote = new System.Windows.Forms.TextBox();
         this.label11 = new System.Windows.Forms.Label();
         this.btnSaveScore = new System.Windows.Forms.Button();
         this.grbSplit.SuspendLayout();
         this.grbUnits.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(29, 42);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(64, 17);
         this.label1.TabIndex = 0;
         this.label1.Text = "Location:";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(29, 69);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(105, 17);
         this.label2.TabIndex = 1;
         this.label2.Text = "Name";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(29, 95);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(105, 17);
         this.label3.TabIndex = 2;
         this.label3.Text = "Date";
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(28, 121);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(105, 17);
         this.label4.TabIndex = 3;
         this.label4.Text = "Gender";
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(29, 145);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(105, 17);
         this.label5.TabIndex = 4;
         this.label5.Text = "Age";
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(29, 195);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(105, 17);
         this.label6.TabIndex = 5;
         this.label6.Text = "Submitted:";
         // 
         // label7
         // 
         this.label7.Location = new System.Drawing.Point(29, 225);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(105, 17);
         this.label7.TabIndex = 6;
         this.label7.Text = "Splits:";
         // 
         // cboLocations
         // 
         this.cboLocations.FormattingEnabled = true;
         this.cboLocations.Location = new System.Drawing.Point(139, 39);
         this.cboLocations.Name = "cboLocations";
         this.cboLocations.Size = new System.Drawing.Size(193, 21);
         this.cboLocations.TabIndex = 7;
         // 
         // txtName
         // 
         this.txtName.Location = new System.Drawing.Point(140, 66);
         this.txtName.Name = "txtName";
         this.txtName.Size = new System.Drawing.Size(192, 20);
         this.txtName.TabIndex = 8;
         // 
         // dtpDateTime
         // 
         this.dtpDateTime.CustomFormat = "mm/dd/jjjj";
         this.dtpDateTime.Location = new System.Drawing.Point(140, 92);
         this.dtpDateTime.Name = "dtpDateTime";
         this.dtpDateTime.Size = new System.Drawing.Size(193, 20);
         this.dtpDateTime.TabIndex = 10;
         // 
         // cboGender
         // 
         this.cboGender.FormattingEnabled = true;
         this.cboGender.Location = new System.Drawing.Point(139, 118);
         this.cboGender.Name = "cboGender";
         this.cboGender.Size = new System.Drawing.Size(194, 21);
         this.cboGender.TabIndex = 11;
         // 
         // txtAge
         // 
         this.txtAge.Location = new System.Drawing.Point(139, 145);
         this.txtAge.Name = "txtAge";
         this.txtAge.Size = new System.Drawing.Size(192, 20);
         this.txtAge.TabIndex = 12;
         // 
         // chbSubmitted
         // 
         this.chbSubmitted.AutoSize = true;
         this.chbSubmitted.Checked = true;
         this.chbSubmitted.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chbSubmitted.Location = new System.Drawing.Point(141, 197);
         this.chbSubmitted.Name = "chbSubmitted";
         this.chbSubmitted.Size = new System.Drawing.Size(15, 14);
         this.chbSubmitted.TabIndex = 13;
         this.chbSubmitted.UseVisualStyleBackColor = true;
         // 
         // lsbSplits
         // 
         this.lsbSplits.FormattingEnabled = true;
         this.lsbSplits.Location = new System.Drawing.Point(30, 245);
         this.lsbSplits.Name = "lsbSplits";
         this.lsbSplits.Size = new System.Drawing.Size(120, 121);
         this.lsbSplits.TabIndex = 14;
         this.lsbSplits.SelectedIndexChanged += new System.EventHandler(this.lsbSplits_SelectedIndexChanged);
         // 
         // btnAddSplit
         // 
         this.btnAddSplit.Location = new System.Drawing.Point(30, 372);
         this.btnAddSplit.Name = "btnAddSplit";
         this.btnAddSplit.Size = new System.Drawing.Size(120, 23);
         this.btnAddSplit.TabIndex = 15;
         this.btnAddSplit.Text = "AddSplit";
         this.btnAddSplit.UseVisualStyleBackColor = true;
         this.btnAddSplit.Click += new System.EventHandler(this.btnAddSplit_Click);
         // 
         // txtDistance
         // 
         this.txtDistance.Location = new System.Drawing.Point(69, 38);
         this.txtDistance.Name = "txtDistance";
         this.txtDistance.Size = new System.Drawing.Size(100, 20);
         this.txtDistance.TabIndex = 16;
         // 
         // txtTime
         // 
         this.txtTime.Location = new System.Drawing.Point(69, 65);
         this.txtTime.Name = "txtTime";
         this.txtTime.Size = new System.Drawing.Size(100, 20);
         this.txtTime.TabIndex = 17;
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(6, 41);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(49, 13);
         this.label8.TabIndex = 18;
         this.label8.Text = "Distance";
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(6, 68);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(30, 13);
         this.label9.TabIndex = 19;
         this.label9.Text = "Time";
         // 
         // grbSplit
         // 
         this.grbSplit.Controls.Add(this.button1);
         this.grbSplit.Controls.Add(this.grbUnits);
         this.grbSplit.Controls.Add(this.txtDistance);
         this.grbSplit.Controls.Add(this.label9);
         this.grbSplit.Controls.Add(this.txtTime);
         this.grbSplit.Controls.Add(this.label8);
         this.grbSplit.Location = new System.Drawing.Point(166, 245);
         this.grbSplit.Name = "grbSplit";
         this.grbSplit.Size = new System.Drawing.Size(415, 121);
         this.grbSplit.TabIndex = 20;
         this.grbSplit.TabStop = false;
         this.grbSplit.Text = "Split";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(323, 19);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 75);
         this.button1.TabIndex = 21;
         this.button1.Text = "Save";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // grbUnits
         // 
         this.grbUnits.Controls.Add(this.cboDistance);
         this.grbUnits.Controls.Add(this.cboTime);
         this.grbUnits.Location = new System.Drawing.Point(175, 19);
         this.grbUnits.Name = "grbUnits";
         this.grbUnits.Size = new System.Drawing.Size(133, 75);
         this.grbUnits.TabIndex = 20;
         this.grbUnits.TabStop = false;
         this.grbUnits.Text = "Units";
         // 
         // cboDistance
         // 
         this.cboDistance.FormattingEnabled = true;
         this.cboDistance.Location = new System.Drawing.Point(6, 19);
         this.cboDistance.Name = "cboDistance";
         this.cboDistance.Size = new System.Drawing.Size(121, 21);
         this.cboDistance.TabIndex = 1;
         // 
         // cboTime
         // 
         this.cboTime.FormattingEnabled = true;
         this.cboTime.Location = new System.Drawing.Point(6, 45);
         this.cboTime.Name = "cboTime";
         this.cboTime.Size = new System.Drawing.Size(121, 21);
         this.cboTime.TabIndex = 0;
         // 
         // txtNat
         // 
         this.txtNat.Location = new System.Drawing.Point(139, 171);
         this.txtNat.Name = "txtNat";
         this.txtNat.Size = new System.Drawing.Size(192, 20);
         this.txtNat.TabIndex = 24;
         // 
         // label10
         // 
         this.label10.Location = new System.Drawing.Point(29, 171);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(105, 17);
         this.label10.TabIndex = 23;
         this.label10.Text = "Nationality";
         // 
         // txtNote
         // 
         this.txtNote.Location = new System.Drawing.Point(398, 43);
         this.txtNote.Multiline = true;
         this.txtNote.Name = "txtNote";
         this.txtNote.Size = new System.Drawing.Size(192, 148);
         this.txtNote.TabIndex = 26;
         // 
         // label11
         // 
         this.label11.Location = new System.Drawing.Point(344, 43);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(105, 17);
         this.label11.TabIndex = 25;
         this.label11.Text = "Note:";
         // 
         // btnSaveScore
         // 
         this.btnSaveScore.Location = new System.Drawing.Point(689, 384);
         this.btnSaveScore.Name = "btnSaveScore";
         this.btnSaveScore.Size = new System.Drawing.Size(99, 54);
         this.btnSaveScore.TabIndex = 27;
         this.btnSaveScore.Text = "SaveScore";
         this.btnSaveScore.UseVisualStyleBackColor = true;
         this.btnSaveScore.Click += new System.EventHandler(this.button2_Click);
         // 
         // AddScore
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.btnSaveScore);
         this.Controls.Add(this.txtNote);
         this.Controls.Add(this.label11);
         this.Controls.Add(this.txtNat);
         this.Controls.Add(this.label10);
         this.Controls.Add(this.grbSplit);
         this.Controls.Add(this.btnAddSplit);
         this.Controls.Add(this.lsbSplits);
         this.Controls.Add(this.chbSubmitted);
         this.Controls.Add(this.txtAge);
         this.Controls.Add(this.cboGender);
         this.Controls.Add(this.dtpDateTime);
         this.Controls.Add(this.txtName);
         this.Controls.Add(this.cboLocations);
         this.Controls.Add(this.label7);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Name = "AddScore";
         this.Text = "AddScore";
         this.Leave += new System.EventHandler(this.AddScore_Leave);
         this.grbSplit.ResumeLayout(false);
         this.grbSplit.PerformLayout();
         this.grbUnits.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private System.Windows.Forms.DateTimePicker dtpDateTime;
      private System.Windows.Forms.ComboBox cboGender;

      private System.Windows.Forms.TextBox txtName;

      private System.Windows.Forms.ComboBox cboLocations;

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label7;

        #endregion

        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.CheckBox chbSubmitted;
        private System.Windows.Forms.ListBox lsbSplits;
        private System.Windows.Forms.Button btnAddSplit;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grbSplit;
        private System.Windows.Forms.GroupBox grbUnits;
        private System.Windows.Forms.ComboBox cboDistance;
        private System.Windows.Forms.ComboBox cboTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSaveScore;
    }
}