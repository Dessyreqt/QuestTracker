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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.uncomplete = new System.Windows.Forms.Button();
            this.complete = new System.Windows.Forms.Button();
            this.addGroup = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.addQuest = new System.Windows.Forms.Button();
            this.quests = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showCompleted = new System.Windows.Forms.CheckBox();
            this.completeDate = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.Label();
            this.questDescription = new System.Windows.Forms.TextBox();
            this.bottomPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uncomplete);
            this.bottomPanel.Controls.Add(this.complete);
            this.bottomPanel.Controls.Add(this.addGroup);
            this.bottomPanel.Controls.Add(this.delete);
            this.bottomPanel.Controls.Add(this.addQuest);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 470);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(787, 46);
            this.bottomPanel.TabIndex = 0;
            // 
            // uncomplete
            // 
            this.uncomplete.BackColor = System.Drawing.SystemColors.Control;
            this.uncomplete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.uncomplete.Location = new System.Drawing.Point(545, 6);
            this.uncomplete.Name = "uncomplete";
            this.uncomplete.Size = new System.Drawing.Size(122, 28);
            this.uncomplete.TabIndex = 4;
            this.uncomplete.Text = "Uncomplete Checked";
            this.uncomplete.UseVisualStyleBackColor = true;
            this.uncomplete.Click += new System.EventHandler(this.uncomplete_Click);
            // 
            // complete
            // 
            this.complete.BackColor = System.Drawing.SystemColors.Control;
            this.complete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.complete.Location = new System.Drawing.Point(228, 6);
            this.complete.Name = "complete";
            this.complete.Size = new System.Drawing.Size(110, 28);
            this.complete.TabIndex = 3;
            this.complete.Text = "Complete Checked";
            this.complete.UseVisualStyleBackColor = true;
            this.complete.Click += new System.EventHandler(this.complete_Click);
            // 
            // addGroup
            // 
            this.addGroup.Location = new System.Drawing.Point(120, 6);
            this.addGroup.Name = "addGroup";
            this.addGroup.Size = new System.Drawing.Size(102, 28);
            this.addGroup.TabIndex = 2;
            this.addGroup.Text = "Add Group";
            this.addGroup.UseVisualStyleBackColor = true;
            this.addGroup.Click += new System.EventHandler(this.addGroup_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.SystemColors.Control;
            this.delete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.delete.Location = new System.Drawing.Point(673, 6);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(102, 28);
            this.delete.TabIndex = 1;
            this.delete.Text = "Delete Checked";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // addQuest
            // 
            this.addQuest.Location = new System.Drawing.Point(12, 6);
            this.addQuest.Name = "addQuest";
            this.addQuest.Size = new System.Drawing.Size(102, 28);
            this.addQuest.TabIndex = 0;
            this.addQuest.Text = "Add Quest";
            this.addQuest.UseVisualStyleBackColor = true;
            this.addQuest.Click += new System.EventHandler(this.addQuest_Click);
            // 
            // quests
            // 
            this.quests.AutoScroll = true;
            this.quests.BackColor = System.Drawing.SystemColors.Window;
            this.quests.Dock = System.Windows.Forms.DockStyle.Left;
            this.quests.Location = new System.Drawing.Point(0, 0);
            this.quests.Name = "quests";
            this.quests.Size = new System.Drawing.Size(393, 470);
            this.quests.TabIndex = 1;
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(393, 0);
            this.splitter.MinExtra = 150;
            this.splitter.MinSize = 150;
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(10, 470);
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
            this.panel1.Location = new System.Drawing.Point(403, 0);
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
            this.questDescription.Location = new System.Drawing.Point(403, 73);
            this.questDescription.Multiline = true;
            this.questDescription.Name = "questDescription";
            this.questDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.questDescription.Size = new System.Drawing.Size(384, 397);
            this.questDescription.TabIndex = 5;
            this.questDescription.TextChanged += new System.EventHandler(this.questDescription_TextChanged);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel quests;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Button addQuest;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button addGroup;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox questDescription;
        public System.Windows.Forms.Label startDate;
        public System.Windows.Forms.Label completeDate;
        private System.Windows.Forms.Button complete;
        private System.Windows.Forms.Button uncomplete;
        public System.Windows.Forms.CheckBox showCompleted;




    }
}

