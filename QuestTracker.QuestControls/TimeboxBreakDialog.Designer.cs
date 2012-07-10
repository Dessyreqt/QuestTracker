namespace QuestTracker.QuestControls
{
    partial class TimeboxBreakDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.startTask = new System.Windows.Forms.Button();
            this.continueBreak = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Break is done! Back to work!";
            // 
            // startTask
            // 
            this.startTask.Location = new System.Drawing.Point(13, 36);
            this.startTask.Name = "startTask";
            this.startTask.Size = new System.Drawing.Size(75, 23);
            this.startTask.TabIndex = 1;
            this.startTask.Text = "Start Task";
            this.startTask.UseVisualStyleBackColor = true;
            this.startTask.Click += new System.EventHandler(this.startTask_Click);
            // 
            // continueBreak
            // 
            this.continueBreak.Location = new System.Drawing.Point(94, 36);
            this.continueBreak.Name = "continueBreak";
            this.continueBreak.Size = new System.Drawing.Size(97, 23);
            this.continueBreak.TabIndex = 2;
            this.continueBreak.Text = "Continue Break";
            this.continueBreak.UseVisualStyleBackColor = true;
            this.continueBreak.Click += new System.EventHandler(this.continueBreak_Click);
            // 
            // TimeboxBreakDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 80);
            this.Controls.Add(this.continueBreak);
            this.Controls.Add(this.startTask);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeboxBreakDialog";
            this.Text = "Break Done";
            this.Load += new System.EventHandler(this.TimeboxBreakDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startTask;
        private System.Windows.Forms.Button continueBreak;
    }
}