namespace Scheduler {
    partial class CustomerForm {
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
            label_name = new Label();
            textBox_name = new TextBox();
            checkBox_active = new CheckBox();
            label_address1 = new Label();
            textBox_address1 = new TextBox();
            label_address2 = new Label();
            textBox_address2 = new TextBox();
            label_city = new Label();
            label_country = new Label();
            textBox_city = new TextBox();
            textBox_country = new TextBox();
            label_postcode = new Label();
            label_phone = new Label();
            textBox_postcode = new TextBox();
            textBox_phone = new TextBox();
            button_save = new Button();
            button_appt = new Button();
            SuspendLayout();
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Location = new Point(12, 9);
            label_name.Name = "label_name";
            label_name.Size = new Size(94, 15);
            label_name.TabIndex = 0;
            label_name.Text = "Customer Name";
            // 
            // textBox_name
            // 
            textBox_name.Location = new Point(12, 27);
            textBox_name.Name = "textBox_name";
            textBox_name.Size = new Size(243, 23);
            textBox_name.TabIndex = 1;
            // 
            // checkBox_active
            // 
            checkBox_active.AutoSize = true;
            checkBox_active.Checked = true;
            checkBox_active.CheckState = CheckState.Checked;
            checkBox_active.Location = new Point(261, 27);
            checkBox_active.Name = "checkBox_active";
            checkBox_active.Size = new Size(59, 19);
            checkBox_active.TabIndex = 2;
            checkBox_active.Text = "Active";
            checkBox_active.UseVisualStyleBackColor = true;
            // 
            // label_address1
            // 
            label_address1.AutoSize = true;
            label_address1.Location = new Point(12, 53);
            label_address1.Name = "label_address1";
            label_address1.Size = new Size(83, 15);
            label_address1.TabIndex = 3;
            label_address1.Text = "Address Line 1";
            // 
            // textBox_address1
            // 
            textBox_address1.Location = new Point(12, 71);
            textBox_address1.Name = "textBox_address1";
            textBox_address1.Size = new Size(308, 23);
            textBox_address1.TabIndex = 4;
            // 
            // label_address2
            // 
            label_address2.AutoSize = true;
            label_address2.Location = new Point(12, 97);
            label_address2.Name = "label_address2";
            label_address2.Size = new Size(83, 15);
            label_address2.TabIndex = 5;
            label_address2.Text = "Address Line 2";
            // 
            // textBox_address2
            // 
            textBox_address2.Location = new Point(12, 115);
            textBox_address2.Name = "textBox_address2";
            textBox_address2.Size = new Size(308, 23);
            textBox_address2.TabIndex = 6;
            // 
            // label_city
            // 
            label_city.AutoSize = true;
            label_city.Location = new Point(12, 141);
            label_city.Name = "label_city";
            label_city.Size = new Size(28, 15);
            label_city.TabIndex = 7;
            label_city.Text = "City";
            // 
            // label_country
            // 
            label_country.AutoSize = true;
            label_country.Location = new Point(161, 141);
            label_country.Name = "label_country";
            label_country.Size = new Size(50, 15);
            label_country.TabIndex = 8;
            label_country.Text = "Country";
            // 
            // textBox_city
            // 
            textBox_city.Location = new Point(12, 159);
            textBox_city.Name = "textBox_city";
            textBox_city.Size = new Size(143, 23);
            textBox_city.TabIndex = 9;
            // 
            // textBox_country
            // 
            textBox_country.Location = new Point(161, 159);
            textBox_country.Name = "textBox_country";
            textBox_country.Size = new Size(159, 23);
            textBox_country.TabIndex = 10;
            // 
            // label_postcode
            // 
            label_postcode.AutoSize = true;
            label_postcode.Location = new Point(12, 185);
            label_postcode.Name = "label_postcode";
            label_postcode.Size = new Size(70, 15);
            label_postcode.TabIndex = 11;
            label_postcode.Text = "Postal Code";
            // 
            // label_phone
            // 
            label_phone.AutoSize = true;
            label_phone.Location = new Point(161, 185);
            label_phone.Name = "label_phone";
            label_phone.Size = new Size(41, 15);
            label_phone.TabIndex = 12;
            label_phone.Text = "Phone";
            // 
            // textBox_postcode
            // 
            textBox_postcode.Location = new Point(12, 203);
            textBox_postcode.Name = "textBox_postcode";
            textBox_postcode.Size = new Size(143, 23);
            textBox_postcode.TabIndex = 13;
            // 
            // textBox_phone
            // 
            textBox_phone.Location = new Point(161, 203);
            textBox_phone.Name = "textBox_phone";
            textBox_phone.Size = new Size(159, 23);
            textBox_phone.TabIndex = 14;
            // 
            // button_save
            // 
            button_save.Location = new Point(161, 240);
            button_save.Name = "button_save";
            button_save.Size = new Size(159, 23);
            button_save.TabIndex = 15;
            button_save.Text = "Save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // button_appt
            // 
            button_appt.Location = new Point(12, 240);
            button_appt.Name = "button_appt";
            button_appt.Size = new Size(143, 23);
            button_appt.TabIndex = 16;
            button_appt.Text = "Create Appointment";
            button_appt.UseVisualStyleBackColor = true;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 275);
            Controls.Add(button_appt);
            Controls.Add(button_save);
            Controls.Add(textBox_phone);
            Controls.Add(textBox_postcode);
            Controls.Add(label_phone);
            Controls.Add(label_postcode);
            Controls.Add(textBox_country);
            Controls.Add(textBox_city);
            Controls.Add(label_country);
            Controls.Add(label_city);
            Controls.Add(textBox_address2);
            Controls.Add(label_address2);
            Controls.Add(textBox_address1);
            Controls.Add(label_address1);
            Controls.Add(checkBox_active);
            Controls.Add(textBox_name);
            Controls.Add(label_name);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerForm";
            Text = "Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_name;
        private TextBox textBox_name;
        private CheckBox checkBox_active;
        private Label label_address1;
        private TextBox textBox_address1;
        private Label label_address2;
        private TextBox textBox_address2;
        private Label label_city;
        private Label label_country;
        private TextBox textBox_city;
        private TextBox textBox_country;
        private Label label_postcode;
        private Label label_phone;
        private TextBox textBox_postcode;
        private TextBox textBox_phone;
        private Button button_save;
        private Button button_appt;
    }
}