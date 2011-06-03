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
            this.components = new System.ComponentModel.Container();
            this.questTabs = new System.Windows.Forms.TabControl();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage = new System.Windows.Forms.TabPage();
            this.addTab = new System.Windows.Forms.TabPage();
            this.rename = new System.Windows.Forms.TextBox();
            this.questTabControl = new QuestTracker.QuestControls.QuestTabControl();
            this.questTabs.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // questTabs
            // 
            this.questTabs.AllowDrop = true;
            this.questTabs.ContextMenuStrip = this.contextMenu;
            this.questTabs.Controls.Add(this.tabPage);
            this.questTabs.Controls.Add(this.addTab);
            this.questTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questTabs.Location = new System.Drawing.Point(0, 0);
            this.questTabs.Name = "questTabs";
            this.questTabs.SelectedIndex = 0;
            this.questTabs.Size = new System.Drawing.Size(379, 392);
            this.questTabs.TabIndex = 0;
            this.questTabs.SelectedIndexChanged += new System.EventHandler(this.questTabs_SelectedIndexChanged);
            this.questTabs.Click += new System.EventHandler(this.questTabs_Click);
            this.questTabs.DragOver += new System.Windows.Forms.DragEventHandler(this.questTabs_DragOver);
            this.questTabs.DoubleClick += new System.EventHandler(this.questTabs_DoubleClick);
            this.questTabs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.questTabs_MouseMove);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeTabToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(160, 26);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // removeTabToolStripMenuItem
            // 
            this.removeTabToolStripMenuItem.Name = "removeTabToolStripMenuItem";
            this.removeTabToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.removeTabToolStripMenuItem.Text = "Remove this tab";
            this.removeTabToolStripMenuItem.Click += new System.EventHandler(this.removeTabToolStripMenuItem_Click);
            // 
            // tabPage
            // 
            this.tabPage.ContextMenuStrip = this.contextMenu;
            this.tabPage.Controls.Add(this.questTabControl);
            this.tabPage.Location = new System.Drawing.Point(4, 22);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage.Size = new System.Drawing.Size(371, 366);
            this.tabPage.TabIndex = 0;
            this.tabPage.Text = "Default Quests";
            this.tabPage.UseVisualStyleBackColor = true;
            // 
            // addTab
            // 
            this.addTab.Location = new System.Drawing.Point(4, 22);
            this.addTab.Name = "addTab";
            this.addTab.Padding = new System.Windows.Forms.Padding(3);
            this.addTab.Size = new System.Drawing.Size(371, 366);
            this.addTab.TabIndex = 1;
            this.addTab.Text = "Add Tab";
            this.addTab.UseVisualStyleBackColor = true;
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(-1, 0);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(100, 20);
            this.rename.TabIndex = 2;
            this.rename.Text = "Default Quests";
            this.rename.Visible = false;
            this.rename.TextChanged += new System.EventHandler(this.rename_TextChanged);
            this.rename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rename_KeyDown);
            this.rename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rename_KeyPress);
            this.rename.Leave += new System.EventHandler(this.rename_Leave);
            // 
            // questTabControl
            // 
            this.questTabControl.AllCheckedComplete = false;
            this.questTabControl.AnyChecked = false;
            this.questTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questTabControl.LastSelectedQuest = null;
            this.questTabControl.LastSelectedQuestControl = null;
            this.questTabControl.LastSelectedQuestGroup = null;
            this.questTabControl.LastSelectedQuestGroupControl = null;
            this.questTabControl.Location = new System.Drawing.Point(3, 3);
            this.questTabControl.Name = "questTabControl";
            this.questTabControl.QuestTab = null;
            this.questTabControl.Size = new System.Drawing.Size(365, 360);
            this.questTabControl.TabIndex = 0;
            this.questTabControl.SelectionPluralityChanged += new System.EventHandler(this.questTabControl_SelectionPluralityChanged);
            this.questTabControl.QuestSelectionChanged += new System.EventHandler(this.questTabControl_QuestSelectionChanged);
            // 
            // QuestLogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rename);
            this.Controls.Add(this.questTabs);
            this.Name = "QuestLogControl";
            this.Size = new System.Drawing.Size(379, 392);
            this.questTabs.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl questTabs;
        private System.Windows.Forms.TabPage tabPage;
        private System.Windows.Forms.TabPage addTab;
        private System.Windows.Forms.TextBox rename;
        private QuestTabControl questTabControl;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem removeTabToolStripMenuItem;
    }
}
