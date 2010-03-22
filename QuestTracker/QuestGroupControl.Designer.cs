namespace QuestTracker
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
            this.panel = new System.Windows.Forms.Panel();
            this.rename = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.Label();
            this.selected = new System.Windows.Forms.CheckBox();
            this.expand = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
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
            this.panel.Leave += new System.EventHandler(this.QuestGroupControl_Leave);
            this.panel.Resize += new System.EventHandler(this.panel_Resize);
            this.panel.Enter += new System.EventHandler(this.QuestGroupControl_Enter);
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
            this.rename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rename_KeyPress);
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.SystemColors.Control;
            this.name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.name.Location = new System.Drawing.Point(38, 0);
            this.name.Name = "name";
            this.name.Padding = new System.Windows.Forms.Padding(5);
            this.name.Size = new System.Drawing.Size(308, 24);
            this.name.TabIndex = 5;
            this.name.Text = "Unnamed Quest Group";
            this.name.DoubleClick += new System.EventHandler(this.name_DoubleClick);
            this.name.Click += new System.EventHandler(this.name_Click);
            // 
            // selected
            // 
            this.selected.BackColor = System.Drawing.SystemColors.Control;
            this.selected.Dock = System.Windows.Forms.DockStyle.Left;
            this.selected.Location = new System.Drawing.Point(23, 0);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(15, 24);
            this.selected.TabIndex = 4;
            this.selected.UseVisualStyleBackColor = false;
            this.selected.CheckedChanged += new System.EventHandler(this.selected_CheckedChanged);
            // 
            // expand
            // 
            this.expand.BackColor = System.Drawing.SystemColors.Control;
            this.expand.Dock = System.Windows.Forms.DockStyle.Left;
            this.expand.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expand.Image = global::QuestTracker.Properties.Resources.collapse;
            this.expand.Location = new System.Drawing.Point(0, 0);
            this.expand.Name = "expand";
            this.expand.Size = new System.Drawing.Size(23, 24);
            this.expand.TabIndex = 3;
            this.expand.Click += new System.EventHandler(this.expand_Click);
            // 
            // QuestGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "QuestGroupControl";
            this.Size = new System.Drawing.Size(346, 24);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label expand;
        private System.Windows.Forms.TextBox rename;
        private System.Windows.Forms.Label name;
        public System.Windows.Forms.CheckBox selected;
        private System.Windows.Forms.Panel panel;
    }
}
