namespace QuestTracker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.complete = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.quests = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showCompleted = new System.Windows.Forms.CheckBox();
            this.completeDate = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.Label();
            this.questDescription = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questTrackerWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recurringQuestWorker = new System.ComponentModel.BackgroundWorker();
            this.bottomPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.complete);
            this.bottomPanel.Controls.Add(this.delete);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 470);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(787, 46);
            this.bottomPanel.TabIndex = 0;
            // 
            // complete
            // 
            this.complete.BackColor = System.Drawing.SystemColors.Control;
            this.complete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.complete.Location = new System.Drawing.Point(12, 6);
            this.complete.Name = "complete";
            this.complete.Size = new System.Drawing.Size(130, 28);
            this.complete.TabIndex = 3;
            this.complete.Text = "Complete";
            this.complete.UseVisualStyleBackColor = true;
            this.complete.Click += new System.EventHandler(this.complete_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.SystemColors.Control;
            this.delete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.delete.Location = new System.Drawing.Point(673, 6);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(102, 28);
            this.delete.TabIndex = 1;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // quests
            // 
            this.quests.AutoScroll = true;
            this.quests.BackColor = System.Drawing.SystemColors.Window;
            this.quests.Dock = System.Windows.Forms.DockStyle.Left;
            this.quests.Location = new System.Drawing.Point(0, 24);
            this.quests.Name = "quests";
            this.quests.Size = new System.Drawing.Size(393, 446);
            this.quests.TabIndex = 1;
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(393, 24);
            this.splitter.MinExtra = 150;
            this.splitter.MinSize = 150;
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(10, 446);
            this.splitter.TabIndex = 2;
            this.splitter.TabStop = false;
            this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.showCompleted);
            this.panel1.Controls.Add(this.completeDate);
            this.panel1.Controls.Add(this.startDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(403, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 73);
            this.panel1.TabIndex = 4;
            // 
            // showCompleted
            // 
            this.showCompleted.AutoSize = true;
            this.showCompleted.Location = new System.Drawing.Point(9, 50);
            this.showCompleted.Name = "showCompleted";
            this.showCompleted.Size = new System.Drawing.Size(142, 17);
            this.showCompleted.TabIndex = 2;
            this.showCompleted.Text = "Show Completed Quests";
            this.showCompleted.UseVisualStyleBackColor = true;
            this.showCompleted.CheckedChanged += new System.EventHandler(this.showCompleted_CheckedChanged);
            // 
            // completeDate
            // 
            this.completeDate.AutoSize = true;
            this.completeDate.ForeColor = System.Drawing.Color.Red;
            this.completeDate.Location = new System.Drawing.Point(6, 31);
            this.completeDate.Name = "completeDate";
            this.completeDate.Size = new System.Drawing.Size(77, 13);
            this.completeDate.TabIndex = 1;
            this.completeDate.Text = "Not Completed";
            this.completeDate.Visible = false;
            // 
            // startDate
            // 
            this.startDate.AutoSize = true;
            this.startDate.Location = new System.Drawing.Point(6, 9);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(70, 13);
            this.startDate.TabIndex = 0;
            this.startDate.Text = "Date Started:";
            // 
            // questDescription
            // 
            this.questDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questDescription.Enabled = false;
            this.questDescription.Location = new System.Drawing.Point(403, 97);
            this.questDescription.Multiline = true;
            this.questDescription.Name = "questDescription";
            this.questDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.questDescription.Size = new System.Drawing.Size(384, 373);
            this.questDescription.TabIndex = 5;
            this.questDescription.TextChanged += new System.EventHandler(this.questDescription_TextChanged);
            this.questDescription.Enter += new System.EventHandler(this.questDescription_Enter);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.questTrackerWebsiteToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // questTrackerWebsiteToolStripMenuItem
            // 
            this.questTrackerWebsiteToolStripMenuItem.Name = "questTrackerWebsiteToolStripMenuItem";
            this.questTrackerWebsiteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.questTrackerWebsiteToolStripMenuItem.Text = "QuestTracker Website";
            this.questTrackerWebsiteToolStripMenuItem.Click += new System.EventHandler(this.questTrackerWebsiteToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.donateToolStripMenuItem.Text = "Donate...";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // recurringQuestWorker
            // 
            this.recurringQuestWorker.WorkerReportsProgress = true;
            this.recurringQuestWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.recurringQuestWorker_DoWork);
            this.recurringQuestWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.recurringQuestWorker_ProgressChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 516);
            this.Controls.Add(this.questDescription);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.quests);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(625, 325);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "QuestTracker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.bottomPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
        public System.Windows.Forms.Panel quests;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox questDescription;
        public System.Windows.Forms.Label startDate;
        public System.Windows.Forms.Label completeDate;
        private System.Windows.Forms.Button complete;
        public System.Windows.Forms.CheckBox showCompleted;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questTrackerWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker recurringQuestWorker;




    }
}

