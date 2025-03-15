using System.Windows.Forms;

namespace Scheduler {
    partial class MainScreen {
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
            label_username = new Label();
            dataGridView_customers = new DataGridView();
            button_add_customer = new Button();
            dataGridView_appts = new DataGridView();
            label_customers = new Label();
            label_appointments = new Label();
            button_report1 = new Button();
            button_report2 = new Button();
            button_report3 = new Button();
            dateTimePicker_appts = new DateTimePicker();
            comboBox_appts_filter = new ComboBox();
            label_appts_filter = new Label();
            button_appt = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_customers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_appts).BeginInit();
            SuspendLayout();
            // 
            // label_username
            // 
            label_username.AutoSize = true;
            label_username.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_username.Location = new Point(12, 28);
            label_username.Name = "label_username";
            label_username.Size = new Size(267, 25);
            label_username.TabIndex = 0;
            label_username.Text = "Logged in as: xxxxxxxxxxxx";
            // 
            // dataGridView_customers
            // 
            dataGridView_customers.AllowUserToAddRows = false;
            dataGridView_customers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_customers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_customers.Location = new Point(354, 38);
            dataGridView_customers.MultiSelect = false;
            dataGridView_customers.Name = "dataGridView_customers";
            dataGridView_customers.ReadOnly = true;
            dataGridView_customers.RowHeadersVisible = false;
            dataGridView_customers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_customers.Size = new Size(604, 235);
            dataGridView_customers.TabIndex = 1;
            dataGridView_customers.CellDoubleClick += DataGridView_customers_CellDoubleClick;
            dataGridView_customers.SelectionChanged += DataGridView_customers_SelectionChanged;
            // 
            // button_add_customer
            // 
            button_add_customer.Location = new Point(826, 9);
            button_add_customer.Name = "button_add_customer";
            button_add_customer.Size = new Size(132, 23);
            button_add_customer.TabIndex = 2;
            button_add_customer.Text = "Create Customer";
            button_add_customer.UseVisualStyleBackColor = true;
            button_add_customer.Click += button_add_customer_Click;
            // 
            // dataGridView_appts
            // 
            dataGridView_appts.AllowUserToAddRows = false;
            dataGridView_appts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_appts.MultiSelect = false;
            dataGridView_appts.ReadOnly = true;
            dataGridView_appts.RowHeadersVisible = false;
            dataGridView_appts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_appts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_appts.Location = new Point(354, 328);
            dataGridView_appts.Name = "dataGridView_appts";
            dataGridView_appts.Size = new Size(604, 235);
            dataGridView_appts.TabIndex = 3;
            dataGridView_appts.CellDoubleClick += DataGridView_appts_CellDoubleClick;
            // 
            // label_customers
            // 
            label_customers.AutoSize = true;
            label_customers.Location = new Point(354, 13);
            label_customers.Name = "label_customers";
            label_customers.Size = new Size(237, 15);
            label_customers.TabIndex = 5;
            label_customers.Text = "Customers | Double click customer to edit ↓";
            // 
            // label_appointments
            // 
            label_appointments.AutoSize = true;
            label_appointments.Location = new Point(354, 303);
            label_appointments.Name = "label_appointments";
            label_appointments.Size = new Size(343, 15);
            label_appointments.TabIndex = 6;
            label_appointments.Text = "Appointments (Select a Customer to make a new Appointment)";
            // 
            // button_report1
            // 
            button_report1.Location = new Point(12, 80);
            button_report1.Name = "button_report1";
            button_report1.Size = new Size(336, 23);
            button_report1.TabIndex = 7;
            button_report1.Text = "Appts by Type Report";
            button_report1.UseVisualStyleBackColor = true;
            // 
            // button_report2
            // 
            button_report2.Location = new Point(12, 109);
            button_report2.Name = "button_report2";
            button_report2.Size = new Size(336, 23);
            button_report2.TabIndex = 8;
            button_report2.Text = "User Schedules Report";
            button_report2.UseVisualStyleBackColor = true;
            // 
            // button_report3
            // 
            button_report3.Location = new Point(12, 138);
            button_report3.Name = "button_report3";
            button_report3.Size = new Size(336, 23);
            button_report3.TabIndex = 9;
            button_report3.Text = "??? Report";
            button_report3.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker_appts
            // 
            dateTimePicker_appts.Location = new Point(12, 357);
            dateTimePicker_appts.Name = "dateTimePicker_appts";
            dateTimePicker_appts.Size = new Size(336, 23);
            dateTimePicker_appts.TabIndex = 10;
            dateTimePicker_appts.ValueChanged += DateTimePicker_appts_ValueChanged;
            // 
            // comboBox_appts_filter
            // 
            comboBox_appts_filter.FormattingEnabled = true;
            comboBox_appts_filter.Items.AddRange(new object[] { "All Appointments", "This Month", "This Week", "Chosen Day" });
            comboBox_appts_filter.Location = new Point(12, 328);
            comboBox_appts_filter.Name = "comboBox_appts_filter";
            comboBox_appts_filter.Size = new Size(336, 23);
            comboBox_appts_filter.TabIndex = 11;
            comboBox_appts_filter.SelectedIndexChanged += comboBox_appts_filter_SelectedIndexChanged;
            // 
            // label_appts_filter
            // 
            label_appts_filter.AutoSize = true;
            label_appts_filter.Location = new Point(12, 307);
            label_appts_filter.Name = "label_appts_filter";
            label_appts_filter.Size = new Size(159, 15);
            label_appts_filter.TabIndex = 12;
            label_appts_filter.Text = "Showing Appointments for...";
            // 
            // button_appt
            // 
            button_appt.Location = new Point(826, 299);
            button_appt.Name = "button_appt";
            button_appt.Size = new Size(132, 23);
            button_appt.TabIndex = 13;
            button_appt.Text = "Make Appointment";
            button_appt.UseVisualStyleBackColor = true;
            button_appt.Visible = false;
            button_appt.Click += button_appt_Click;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 575);
            Controls.Add(button_appt);
            Controls.Add(label_appts_filter);
            Controls.Add(comboBox_appts_filter);
            Controls.Add(dateTimePicker_appts);
            Controls.Add(button_report3);
            Controls.Add(button_report2);
            Controls.Add(button_report1);
            Controls.Add(label_appointments);
            Controls.Add(label_customers);
            Controls.Add(dataGridView_appts);
            Controls.Add(button_add_customer);
            Controls.Add(dataGridView_customers);
            Controls.Add(label_username);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainScreen";
            Text = "Scheduler";
            ((System.ComponentModel.ISupportInitialize)dataGridView_customers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_appts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void DataGridView_customers_CellClick(object sender, DataGridViewCellEventArgs e) {
            throw new NotImplementedException();
        }

        #endregion

        private Label label_username;
        private DataGridView dataGridView_customers;
        private Button button_add_customer;
        private DataGridView dataGridView_appts;
        private Label label_customers;
        private Label label_appointments;
        private Button button_report1;
        private Button button_report2;
        private Button button_report3;
        private DateTimePicker dateTimePicker_appts;
        private ComboBox comboBox_appts_filter;
        private Label label_appts_filter;
        private Button button_appt;
    }
}