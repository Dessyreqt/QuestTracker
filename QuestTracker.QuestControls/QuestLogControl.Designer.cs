namespace QuestTracker.QuestControls
{
    partial class QuestLogControl
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
            this.quests = new System.Windows.Forms.Panel();
            this.line = new System.Windows.Forms.Panel();
            this.recurringQuestWorker = new System.ComponentModel.BackgroundWorker();
            this.quests.SuspendLayout();
            this.SuspendLayout();
            // 
            // quests
            // 
            this.quests.AllowDrop = true;
            this.quests.AutoScroll = true;
            this.quests.BackColor = System.Drawing.SystemColors.Window;
            this.quests.Controls.Add(this.line);
            this.quests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quests.Location = new System.Drawing.Point(0, 0);
            this.quests.Name = "quests";
            this.quests.Size = new System.Drawing.Size(393, 255);
            this.quests.TabIndex = 2;
            this.quests.DragOver += new System.Windows.Forms.DragEventHandler(this.quests_DragOver);
            this.quests.DragDrop += new System.Windows.Forms.DragEventHandler(this.quests_DragDrop);
            this.quests.DragLeave += new System.EventHandler(this.quests_DragLeave);
            this.quests.DragEnter += new System.Windows.Forms.DragEventHandler(this.quests_DragEnter);
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.Black;
            this.line.Location = new System.Drawing.Point(0, 23);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(393, 2);
            this.line.TabIndex = 7;
            this.line.Visible = false;
            // 
            // recurringQuestWorker
            // 
            this.recurringQuestWorker.WorkerReportsProgress = true;
            this.recurringQuestWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.recurringQuestWorker_DoWork);
            this.recurringQuestWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.recurringQuestWorker_ProgressChanged);
            // 
            // QuestLogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quests);
            this.Name = "QuestLogControl";
            this.Size = new System.Drawing.Size(393, 255);
            this.quests.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel quests;
        private System.Windows.Forms.Panel line;
        private System.ComponentModel.BackgroundWorker recurringQuestWorker;
    }
}
