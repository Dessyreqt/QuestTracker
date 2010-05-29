namespace QuestTracker
{
    partial class AddGroup
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
            this.add = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.SystemColors.Control;
            this.add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add.Location = new System.Drawing.Point(0, 0);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(346, 24);
            this.add.TabIndex = 0;
            this.add.Text = "Add Group";
            this.add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // AddGroup
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.add);
            this.Name = "AddGroup";
            this.Size = new System.Drawing.Size(346, 24);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.AddGroup_DragOver);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.AddGroup_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.AddGroup_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label add;
    }
}
