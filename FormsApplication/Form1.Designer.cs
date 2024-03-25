namespace FormsApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpWelcome = new System.Windows.Forms.TabPage();
            this.btnSettings = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbpScores = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddScore = new System.Windows.Forms.Button();
            this.lsbScores = new System.Windows.Forms.ListBox();
            this.lsbLocations = new System.Windows.Forms.ListBox();
            this.tcMain.SuspendLayout();
            this.tbpWelcome.SuspendLayout();
            this.tbpScores.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpWelcome);
            this.tcMain.Controls.Add(this.tbpScores);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(776, 426);
            this.tcMain.TabIndex = 0;
            // 
            // tbpWelcome
            // 
            this.tbpWelcome.Controls.Add(this.btnSettings);
            this.tbpWelcome.Controls.Add(this.button1);
            this.tbpWelcome.Location = new System.Drawing.Point(4, 22);
            this.tbpWelcome.Name = "tbpWelcome";
            this.tbpWelcome.Padding = new System.Windows.Forms.Padding(3);
            this.tbpWelcome.Size = new System.Drawing.Size(768, 400);
            this.tbpWelcome.TabIndex = 0;
            this.tbpWelcome.Text = "Welcome";
            this.tbpWelcome.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Location = new System.Drawing.Point(677, 353);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(85, 41);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(677, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "LoadScores";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbpScores
            // 
            this.tbpScores.Controls.Add(this.button2);
            this.tbpScores.Controls.Add(this.lblScore);
            this.tbpScores.Controls.Add(this.btnSave);
            this.tbpScores.Controls.Add(this.btnAddScore);
            this.tbpScores.Controls.Add(this.lsbScores);
            this.tbpScores.Controls.Add(this.lsbLocations);
            this.tbpScores.Location = new System.Drawing.Point(4, 22);
            this.tbpScores.Name = "tbpScores";
            this.tbpScores.Padding = new System.Windows.Forms.Padding(3);
            this.tbpScores.Size = new System.Drawing.Size(768, 400);
            this.tbpScores.TabIndex = 1;
            this.tbpScores.Text = "Scores";
            this.tbpScores.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 322);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(176, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "Compress File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblScore
            // 
            this.lblScore.Location = new System.Drawing.Point(381, 8);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(377, 378);
            this.lblScore.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 273);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(176, 43);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Scores";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddScore
            // 
            this.btnAddScore.Location = new System.Drawing.Point(6, 224);
            this.btnAddScore.Name = "btnAddScore";
            this.btnAddScore.Size = new System.Drawing.Size(176, 43);
            this.btnAddScore.TabIndex = 2;
            this.btnAddScore.Text = "Add Scores";
            this.btnAddScore.UseVisualStyleBackColor = true;
            this.btnAddScore.Click += new System.EventHandler(this.btnAddScore_Click);
            // 
            // lsbScores
            // 
            this.lsbScores.FormattingEnabled = true;
            this.lsbScores.Location = new System.Drawing.Point(188, 6);
            this.lsbScores.Name = "lsbScores";
            this.lsbScores.Size = new System.Drawing.Size(176, 381);
            this.lsbScores.TabIndex = 1;
            this.lsbScores.SelectedIndexChanged += new System.EventHandler(this.lsbScores_SelectedIndexChanged);
            // 
            // lsbLocations
            // 
            this.lsbLocations.FormattingEnabled = true;
            this.lsbLocations.Location = new System.Drawing.Point(6, 6);
            this.lsbLocations.Name = "lsbLocations";
            this.lsbLocations.Size = new System.Drawing.Size(176, 212);
            this.lsbLocations.TabIndex = 0;
            this.lsbLocations.SelectedIndexChanged += new System.EventHandler(this.lsbLocations_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tcMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tcMain.ResumeLayout(false);
            this.tbpWelcome.ResumeLayout(false);
            this.tbpScores.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Label lblScore;

        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.Button btnAddScore;

        private System.Windows.Forms.ListBox lsbScores;

        private System.Windows.Forms.Button btnSettings;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tbpWelcome;
        private System.Windows.Forms.TabPage tbpScores;
        private System.Windows.Forms.ListBox lsbLocations;

        #endregion
    }
}