namespace QuestTracker.QuestControls
{
    partial class Timebox
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startTask = new System.Windows.Forms.Button();
            this.startBreak = new System.Windows.Forms.Button();
            this.currentTask = new System.Windows.Forms.Label();
            this.timeRemaining = new System.Windows.Forms.Label();
            this.taskLength = new System.Windows.Forms.TextBox();
            this.breakLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.newTask = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Task:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time Remaining:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Task Length:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Break Length:";
            // 
            // startTask
            // 
            this.startTask.Location = new System.Drawing.Point(15, 110);
            this.startTask.Name = "startTask";
            this.startTask.Size = new System.Drawing.Size(75, 23);
            this.startTask.TabIndex = 4;
            this.startTask.Text = "Start Task";
            this.startTask.UseVisualStyleBackColor = true;
            this.startTask.Click += new System.EventHandler(this.startTask_Click);
            // 
            // startBreak
            // 
            this.startBreak.Location = new System.Drawing.Point(108, 110);
            this.startBreak.Name = "startBreak";
            this.startBreak.Size = new System.Drawing.Size(75, 23);
            this.startBreak.TabIndex = 5;
            this.startBreak.Text = "Start Break";
            this.startBreak.UseVisualStyleBackColor = true;
            this.startBreak.Click += new System.EventHandler(this.startBreak_Click);
            // 
            // currentTask
            // 
            this.currentTask.AutoEllipsis = true;
            this.currentTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTask.Location = new System.Drawing.Point(105, 9);
            this.currentTask.Name = "currentTask";
            this.currentTask.Size = new System.Drawing.Size(170, 13);
            this.currentTask.TabIndex = 6;
            this.currentTask.Text = "<Task>";
            // 
            // timeRemaining
            // 
            this.timeRemaining.AutoSize = true;
            this.timeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeRemaining.Location = new System.Drawing.Point(105, 30);
            this.timeRemaining.Name = "timeRemaining";
            this.timeRemaining.Size = new System.Drawing.Size(78, 13);
            this.timeRemaining.TabIndex = 7;
            this.timeRemaining.Text = "Not Running";
            // 
            // taskLength
            // 
            this.taskLength.Location = new System.Drawing.Point(108, 50);
            this.taskLength.Name = "taskLength";
            this.taskLength.Size = new System.Drawing.Size(49, 20);
            this.taskLength.TabIndex = 8;
            this.taskLength.Text = "25";
            // 
            // breakLength
            // 
            this.breakLength.Location = new System.Drawing.Point(108, 77);
            this.breakLength.Name = "breakLength";
            this.breakLength.Size = new System.Drawing.Size(49, 20);
            this.breakLength.TabIndex = 9;
            this.breakLength.Text = "5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(167, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "minutes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "minutes";
            // 
            // newTask
            // 
            this.newTask.Location = new System.Drawing.Point(200, 110);
            this.newTask.Name = "newTask";
            this.newTask.Size = new System.Drawing.Size(75, 23);
            this.newTask.TabIndex = 12;
            this.newTask.Text = "New Task";
            this.newTask.UseVisualStyleBackColor = true;
            this.newTask.Click += new System.EventHandler(this.newTask_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Timebox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 151);
            this.Controls.Add(this.newTask);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.breakLength);
            this.Controls.Add(this.taskLength);
            this.Controls.Add(this.timeRemaining);
            this.Controls.Add(this.currentTask);
            this.Controls.Add(this.startBreak);
            this.Controls.Add(this.startTask);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Timebox";
            this.Text = "Timebox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startTask;
        private System.Windows.Forms.Button startBreak;
        private System.Windows.Forms.Label currentTask;
        private System.Windows.Forms.Label timeRemaining;
        private System.Windows.Forms.TextBox taskLength;
        private System.Windows.Forms.TextBox breakLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button newTask;
        private System.Windows.Forms.Timer timer;
    }
}