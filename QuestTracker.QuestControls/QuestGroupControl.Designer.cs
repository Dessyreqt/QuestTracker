﻿using System.Windows.Forms;
using QuestTracker.QuestControls;

namespace QuestTracker.QuestControls
{
    partial class QuestGroupControl
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
            this.panel = new System.Windows.Forms.Panel();
            this.rename = new System.Windows.Forms.TextBox();
            this.name = new QuestTracker.QuestControls.FixedLabel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selected = new System.Windows.Forms.CheckBox();
            this.expand = new QuestTracker.QuestControls.FixedLabel();
            this.line = new System.Windows.Forms.Panel();
            this.addQuest = new QuestTracker.QuestControls.AddQuestControl();
            this.timeboxGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.rename);
            this.panel.Controls.Add(this.name);
            this.panel.Controls.Add(this.selected);
            this.panel.Controls.Add(this.expand);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(346, 24);
            this.panel.TabIndex = 0;
            this.panel.Enter += new System.EventHandler(this.QuestGroupControl_Enter);
            this.panel.Leave += new System.EventHandler(this.QuestGroupControl_Leave);
            this.panel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(43, 2);
            this.rename.Multiline = true;
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(100, 20);
            this.rename.TabIndex = 6;
            this.rename.Visible = false;
            this.rename.TextChanged += new System.EventHandler(this.rename_TextChanged);
            this.rename.VisibleChanged += new System.EventHandler(this.rename_VisibleChanged);
            this.rename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rename_KeyDown);
            this.rename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rename_KeyPress);
            // 
            // name
            // 
            this.name.AutoEllipsis = true;
            this.name.BackColor = System.Drawing.SystemColors.Control;
            this.name.ContextMenuStrip = this.contextMenu;
            this.name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.name.Location = new System.Drawing.Point(38, 0);
            this.name.Name = "name";
            this.name.Padding = new System.Windows.Forms.Padding(5);
            this.name.Size = new System.Drawing.Size(308, 24);
            this.name.TabIndex = 5;
            this.name.Text = "Unnamed Quest Group";
            this.name.Click += new System.EventHandler(this.name_Click);
            this.name.DoubleClick += new System.EventHandler(this.name_DoubleClick);
            this.name.MouseMove += new System.Windows.Forms.MouseEventHandler(this.QuestGroupControl_MouseMove);
            this.name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.QuestGroupControl_PreviewKeyDown);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameGroupToolStripMenuItem,
            this.timeboxGroupToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(157, 70);
            // 
            // renameGroupToolStripMenuItem
            // 
            this.renameGroupToolStripMenuItem.Name = "renameGroupToolStripMenuItem";
            this.renameGroupToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.renameGroupToolStripMenuItem.Text = "Rename Group";
            this.renameGroupToolStripMenuItem.Click += new System.EventHandler(this.renameGroupToolStripMenuItem_Click);
            // 
            // selected
            // 
            this.selected.BackColor = System.Drawing.SystemColors.Control;
            this.selected.ContextMenuStrip = this.contextMenu;
            this.selected.Dock = System.Windows.Forms.DockStyle.Left;
            this.selected.Location = new System.Drawing.Point(23, 0);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(15, 24);
            this.selected.TabIndex = 4;
            this.selected.UseVisualStyleBackColor = false;
            this.selected.CheckedChanged += new System.EventHandler(this.selected_CheckedChanged);
            this.selected.MouseMove += new System.Windows.Forms.MouseEventHandler(this.QuestGroupControl_MouseMove);
            this.selected.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.QuestGroupControl_PreviewKeyDown);
            // 
            // expand
            // 
            this.expand.BackColor = System.Drawing.SystemColors.Control;
            this.expand.ContextMenuStrip = this.contextMenu;
            this.expand.Dock = System.Windows.Forms.DockStyle.Left;
            this.expand.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expand.Image = global::QuestTracker.QuestControls.Properties.Resources.collapse;
            this.expand.Location = new System.Drawing.Point(0, 0);
            this.expand.Name = "expand";
            this.expand.Size = new System.Drawing.Size(23, 24);
            this.expand.TabIndex = 3;
            this.expand.Click += new System.EventHandler(this.expand_Click);
            this.expand.MouseMove += new System.Windows.Forms.MouseEventHandler(this.QuestGroupControl_MouseMove);
            this.expand.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.QuestGroupControl_PreviewKeyDown);
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.Black;
            this.line.Location = new System.Drawing.Point(0, 23);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(346, 2);
            this.line.TabIndex = 7;
            this.line.Visible = false;
            // 
            // addQuest
            // 
            this.addQuest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addQuest.Location = new System.Drawing.Point(0, 24);
            this.addQuest.Name = "addQuest";
            this.addQuest.Size = new System.Drawing.Size(346, 24);
            this.addQuest.TabIndex = 1;
            this.addQuest.TabStop = false;
            // 
            // timeboxGroupToolStripMenuItem
            // 
            this.timeboxGroupToolStripMenuItem.Name = "timeboxGroupToolStripMenuItem";
            this.timeboxGroupToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.timeboxGroupToolStripMenuItem.Text = "Timebox Group";
            this.timeboxGroupToolStripMenuItem.Click += new System.EventHandler(this.timeboxGroupToolStripMenuItem_Click);
            // 
            // QuestGroupControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addQuest);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.line);
            this.MinimumSize = new System.Drawing.Size(0, 24);
            this.Name = "QuestGroupControl";
            this.Size = new System.Drawing.Size(346, 48);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.QuestGroupControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.QuestGroupControl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.QuestGroupControl_DragOver);
            this.DragLeave += new System.EventHandler(this.QuestGroupControl_DragLeave);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.QuestGroupControl_PreviewKeyDown);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FixedLabel expand;
        private TextBox rename;
        private FixedLabel name;
        public CheckBox selected;
        private Panel panel;
        private AddQuestControl addQuest;
        private Panel line;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem renameGroupToolStripMenuItem;
        private ToolStripMenuItem timeboxGroupToolStripMenuItem;
    }
}