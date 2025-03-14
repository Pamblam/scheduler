namespace Scheduler {
    partial class AppointmentForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            label_user = new Label();
            label_customer = new Label();
            label_customerName = new Label();
            label_userName = new Label();
            label_apptType = new Label();
            textBox_apptType = new TextBox();
            label_startTime = new Label();
            label_endTime = new Label();
            dateTimePicker_start = new DateTimePicker();
            dateTimePicker_end = new DateTimePicker();
            button_delete = new Button();
            button_save = new Button();
            button_close = new Button();
            SuspendLayout();
            // 
            // label_user
            // 
            label_user.AutoSize = true;
            label_user.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_user.Location = new Point(40, 24);
            label_user.Name = "label_user";
            label_user.Size = new Size(36, 15);
            label_user.TabIndex = 0;
            label_user.Text = "User:";
            // 
            // label_customer
            // 
            label_customer.AutoSize = true;
            label_customer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_customer.Location = new Point(12, 9);
            label_customer.Name = "label_customer";
            label_customer.Size = new Size(64, 15);
            label_customer.TabIndex = 1;
            label_customer.Text = "Customer:";
            // 
            // label_customerName
            // 
            label_customerName.AutoSize = true;
            label_customerName.Location = new Point(82, 9);
            label_customerName.Name = "label_customerName";
            label_customerName.Size = new Size(58, 15);
            label_customerName.TabIndex = 2;
            label_customerName.Text = "xxxx xxxx";
            // 
            // label_userName
            // 
            label_userName.AutoSize = true;
            label_userName.Location = new Point(82, 24);
            label_userName.Name = "label_userName";
            label_userName.Size = new Size(58, 15);
            label_userName.TabIndex = 3;
            label_userName.Text = "xxxx xxxx";
            // 
            // label_apptType
            // 
            label_apptType.AutoSize = true;
            label_apptType.Location = new Point(12, 60);
            label_apptType.Name = "label_apptType";
            label_apptType.Size = new Size(105, 15);
            label_apptType.TabIndex = 4;
            label_apptType.Text = "Appointment Type";
            // 
            // textBox_apptType
            // 
            textBox_apptType.Location = new Point(12, 78);
            textBox_apptType.Name = "textBox_apptType";
            textBox_apptType.Size = new Size(291, 23);
            textBox_apptType.TabIndex = 5;
            // 
            // label_startTime
            // 
            label_startTime.AutoSize = true;
            label_startTime.Location = new Point(12, 104);
            label_startTime.Name = "label_startTime";
            label_startTime.Size = new Size(60, 15);
            label_startTime.TabIndex = 6;
            label_startTime.Text = "Start Time";
            // 
            // label_endTime
            // 
            label_endTime.AutoSize = true;
            label_endTime.Location = new Point(12, 148);
            label_endTime.Name = "label_endTime";
            label_endTime.Size = new Size(56, 15);
            label_endTime.TabIndex = 7;
            label_endTime.Text = "End Time";
            // 
            // dateTimePicker_start
            // 
            dateTimePicker_start.CustomFormat = "MM/dd/yyyy hh:mm tt";
            dateTimePicker_start.Format = DateTimePickerFormat.Custom;
            dateTimePicker_start.Location = new Point(12, 122);
            dateTimePicker_start.Name = "dateTimePicker_start";
            dateTimePicker_start.Size = new Size(291, 23);
            dateTimePicker_start.TabIndex = 8;
            // 
            // dateTimePicker_end
            // 
            dateTimePicker_end.CustomFormat = "MM/dd/yyyy hh:mm tt";
            dateTimePicker_end.Format = DateTimePickerFormat.Custom;
            dateTimePicker_end.Location = new Point(12, 166);
            dateTimePicker_end.Name = "dateTimePicker_end";
            dateTimePicker_end.Size = new Size(291, 23);
            dateTimePicker_end.TabIndex = 9;
            // 
            // button_delete
            // 
            button_delete.Location = new Point(12, 211);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(291, 23);
            button_delete.TabIndex = 10;
            button_delete.Text = "Delete Appointment";
            button_delete.UseVisualStyleBackColor = true;
            button_delete.Click += button_delete_Click;
            // 
            // button_save
            // 
            button_save.Location = new Point(155, 240);
            button_save.Name = "button_save";
            button_save.Size = new Size(148, 23);
            button_save.TabIndex = 11;
            button_save.Text = "Save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // button_close
            // 
            button_close.Location = new Point(12, 240);
            button_close.Name = "button_close";
            button_close.Size = new Size(137, 23);
            button_close.TabIndex = 12;
            button_close.Text = "Close";
            button_close.UseVisualStyleBackColor = true;
            button_close.Click += button_close_Click;
            // 
            // AppointmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 277);
            Controls.Add(button_close);
            Controls.Add(button_save);
            Controls.Add(button_delete);
            Controls.Add(dateTimePicker_end);
            Controls.Add(dateTimePicker_start);
            Controls.Add(label_endTime);
            Controls.Add(label_startTime);
            Controls.Add(textBox_apptType);
            Controls.Add(label_apptType);
            Controls.Add(label_userName);
            Controls.Add(label_customerName);
            Controls.Add(label_customer);
            Controls.Add(label_user);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AppointmentForm";
            Text = "Appointment";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_user;
        private Label label_customer;
        private Label label_customerName;
        private Label label_userName;
        private Label label_apptType;
        private TextBox textBox_apptType;
        private Label label_startTime;
        private Label label_endTime;
        private DateTimePicker dateTimePicker_start;
        private DateTimePicker dateTimePicker_end;
        private Button button_delete;
        private Button button_save;
        private Button button_close;
    }
}