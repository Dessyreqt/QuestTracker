namespace QuestTracker.QuestControls
{
    partial class TimeboxTaskDialog
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
            this.breakTime = new System.Windows.Forms.Button();
            this.continueTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This timebox is done! Take a break!";
            // 
            // breakTime
            // 
            this.breakTime.Location = new System.Drawing.Point(16, 36);
            this.breakTime.Name = "breakTime";
            this.breakTime.Size = new System.Drawing.Size(83, 23);
            this.breakTime.TabIndex = 1;
            this.breakTime.Text = "Break Time";
            this.breakTime.UseVisualStyleBackColor = true;
            this.breakTime.Click += new System.EventHandler(this.breakTime_Click);
            // 
            // continueTask
            // 
            this.continueTask.Location = new System.Drawing.Point(105, 36);
            this.continueTask.Name = "continueTask";
            this.continueTask.Size = new System.Drawing.Size(94, 23);
            this.continueTask.TabIndex = 2;
            this.continueTask.Text = "Continue Task";
            this.continueTask.UseVisualStyleBackColor = true;
            this.continueTask.Click += new System.EventHandler(this.continueTask_Click);
            // 
            // TimeboxTaskDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 80);
            this.Controls.Add(this.continueTask);
            this.Controls.Add(this.breakTime);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeboxTaskDialog";
            this.Text = "Timebox Done";
            this.Load += new System.EventHandler(this.TimeboxTaskDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button breakTime;
        private System.Windows.Forms.Button continueTask;
    }
}