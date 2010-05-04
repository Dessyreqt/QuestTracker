namespace QuestTracker
{
    partial class RecurringQuestDialog
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
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.frequency = new System.Windows.Forms.TextBox();
            this.unit = new System.Windows.Forms.ComboBox();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.LinkLabel();
            this.recur = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting on this date and time:";
            // 
            // startDate
            // 
            this.startDate.CustomFormat = "dddd,MMMMdd, yyyy hh:mm tt ";
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate.Location = new System.Drawing.Point(12, 48);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(242, 20);
            this.startDate.TabIndex = 1;
            this.startDate.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Recur this quest every:";
            // 
            // frequency
            // 
            this.frequency.Location = new System.Drawing.Point(12, 88);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(45, 20);
            this.frequency.TabIndex = 3;
            this.frequency.Text = "0";
            this.frequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.frequency.TextChanged += new System.EventHandler(this.quantity_TextChanged);
            // 
            // unit
            // 
            this.unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unit.FormattingEnabled = true;
            this.unit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days"});
            this.unit.Location = new System.Drawing.Point(63, 87);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(103, 21);
            this.unit.TabIndex = 4;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(169, 114);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(85, 26);
            this.save.TabIndex = 5;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.AutoSize = true;
            this.cancel.Location = new System.Drawing.Point(12, 121);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(40, 13);
            this.cancel.TabIndex = 6;
            this.cancel.TabStop = false;
            this.cancel.Text = "Cancel";
            this.cancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cancel_LinkClicked);
            // 
            // recur
            // 
            this.recur.AutoSize = true;
            this.recur.Location = new System.Drawing.Point(15, 12);
            this.recur.Name = "recur";
            this.recur.Size = new System.Drawing.Size(103, 17);
            this.recur.TabIndex = 7;
            this.recur.Text = "Recur this quest";
            this.recur.UseVisualStyleBackColor = true;
            // 
            // RecurringQuestDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 152);
            this.Controls.Add(this.recur);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.unit);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecurringQuestDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Set Recurring Quest...";
            this.Load += new System.EventHandler(this.RecurringQuestDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox frequency;
        private System.Windows.Forms.ComboBox unit;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.LinkLabel cancel;
        private System.Windows.Forms.CheckBox recur;
    }
}