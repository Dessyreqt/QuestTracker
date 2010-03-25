using System.Windows.Forms;

namespace QuestTracker
{
    partial class QuestControl
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
            this.filler = new FixedLabel();
            this.selected = new System.Windows.Forms.CheckBox();
            this.name = new FixedLabel();
            this.rename = new System.Windows.Forms.TextBox();
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
            this.filler.Click += new System.EventHandler(this.filler_Click);
            // 
            // selected
            // 
            this.selected.BackColor = System.Drawing.SystemColors.Window;
            this.selected.Dock = System.Windows.Forms.DockStyle.Left;
            this.selected.Location = new System.Drawing.Point(46, 0);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(15, 24);
            this.selected.TabIndex = 1;
            this.selected.UseVisualStyleBackColor = false;
            this.selected.CheckedChanged += new System.EventHandler(this.selected_CheckedChanged);
            // 
            // name
            // 
            this.name.AutoEllipsis = true;
            this.name.BackColor = System.Drawing.SystemColors.Window;
            this.name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.name.Location = new System.Drawing.Point(61, 0);
            this.name.Name = "name";
            this.name.Padding = new System.Windows.Forms.Padding(5);
            this.name.Size = new System.Drawing.Size(253, 24);
            this.name.TabIndex = 2;
            this.name.Text = "Unnamed Quest";
            this.name.DoubleClick += new System.EventHandler(this.name_DoubleClick);
            this.name.Click += new System.EventHandler(this.name_Click);
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(66, 2);
            this.rename.Multiline = true;
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(100, 20);
            this.rename.TabIndex = 3;
            this.rename.Visible = false;
            this.rename.TextChanged += new System.EventHandler(this.rename_TextChanged);
            this.rename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rename_KeyDown);
            this.rename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rename_KeyPress);
            // 
            // QuestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rename);
            this.Controls.Add(this.name);
            this.Controls.Add(this.selected);
            this.Controls.Add(this.filler);
            this.Name = "QuestControl";
            this.Size = new System.Drawing.Size(314, 24);
            this.Leave += new System.EventHandler(this.QuestControl_Leave);
            this.Resize += new System.EventHandler(this.QuestControl_Resize);
            this.Enter += new System.EventHandler(this.QuestControl_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FixedLabel filler;
        public CheckBox selected;
        private FixedLabel name;
        private TextBox rename;

    }
}
