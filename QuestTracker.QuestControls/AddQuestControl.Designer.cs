namespace QuestTracker.QuestControls
{
    partial class AddQuestControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filler = new System.Windows.Forms.Label();
            this.add = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filler
            // 
            this.filler.BackColor = System.Drawing.SystemColors.Window;
            this.filler.Dock = System.Windows.Forms.DockStyle.Left;
            this.filler.Location = new System.Drawing.Point(0, 0);
            this.filler.Name = "filler";
            this.filler.Size = new System.Drawing.Size(46, 24);
            this.filler.TabIndex = 0;
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.SystemColors.Window;
            this.add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add.Location = new System.Drawing.Point(46, 0);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(300, 24);
            this.add.TabIndex = 1;
            this.add.Text = "Add Quest";
            this.add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // AddQuest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.add);
            this.Controls.Add(this.filler);
            this.Name = "AddQuest";
            this.Size = new System.Drawing.Size(346, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label filler;
        private System.Windows.Forms.Label add;
    }
}