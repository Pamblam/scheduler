using Mysqlx.Crud;
using MySqlX.XDevAPI.Relational;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Scheduler {
    public partial class MainScreen : Form {
        int? selectedCustomerId = null;

        public MainScreen() {

            string version_dotnet = System.Environment.Version.ToString();
            string version_os = Environment.OSVersion.ToString();
            string current_culture = System.Globalization.CultureInfo.CurrentCulture.Name;
            string current_ui_culture = System.Globalization.CultureInfo.CurrentUICulture.Name;
            string version_windows_forms = typeof(System.Windows.Forms.Form).Assembly.GetName().Version.ToString();

            Debug.WriteLine($"\ndotnet version: {version_dotnet}\n"+
                $"os version: {version_os}\n"+
                $"culture: {current_culture}\n"+
                $"ui culture: {current_ui_culture}\n"+
                $"WF version: {version_windows_forms}\n");

            // Lab environment:
            // dotnet version: 8.0.7
            // os version: Microsoft Windows NT 10.0.19045.0
            // culture: en-US
            // ui culture: en-US
            // WF version: 8.0.0.0

            InitializeComponent();
            label_username.Text = "";
            Form_login loginForm = new Form_login(this);
            loginForm.ShowDialog();
        }

        public void populateWindow() {
            comboBox_appts_filter.SelectedIndex = 0;
            dateTimePicker_appts.Visible = false;
            label_username.Text = $"Logged in as: {DataAccessLayer.User.userName}";
            populateCustomersGrid();
            populateSchedulesGrid();
        }

        public void CheckForUpcomingAppts() {
            DataTable upcoming = DataAccessLayer.GetUpcomingAppointments();
            if (upcoming.Rows.Count > 0) {

                StringBuilder sb = new StringBuilder();
                sb.Append($"You have {upcoming.Rows.Count} upcoming appointment(s):");
                sb.AppendLine();
                int counter = 1;

                foreach (DataRow row in upcoming.Rows) {
                    DateTime dateTimeValue = Convert.ToDateTime(row[2]);
                    string startTime = dateTimeValue.ToString("hh:mm tt");
                    sb.Append($"  {counter}. {row[3]} appointment with {row[1]} at {startTime}.");
                    counter++;
                    sb.AppendLine();
                }
                sb.Append($"Would you like to view the first one now?");

                string message = sb.ToString();

                DialogResult result = MessageBox.Show(
                    message,
                    "Upcoming Appointments",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation
                );
                if (result == DialogResult.Yes) {
                    AppointmentForm f = new AppointmentForm(this, (int)upcoming.Rows[0][0], null);
                    f.ShowDialog();
                }

            }
        }

        private void button_add_customer_Click(object sender, EventArgs e) {
            CustomerForm customerForm = new CustomerForm(this, null);
            customerForm.ShowDialog();
        }

        public void populateCustomersGrid() {
            DataTable customers = DataAccessLayer.GetCustomerDataTable();
            dataGridView_customers.DataSource = customers;
            initNewApptBtn();
        }

        public void populateSchedulesGrid() {
            var selectedValue = comboBox_appts_filter.SelectedItem;
            if ("This Month" == selectedValue) {
                DataTable appts = DataAccessLayer.GetAllAppointmentsThisMonthDataTable();
                dataGridView_appts.DataSource = appts;
            } else if ("This Week" == selectedValue) {
                DataTable appts = DataAccessLayer.GetAllAppointmentsThisWeekDataTable();
                dataGridView_appts.DataSource = appts;
            } else if ("Chosen Day" == selectedValue) {
                DateTime day = dateTimePicker_appts.Value.Date.ToUniversalTime();
                DataTable appts = DataAccessLayer.GetAllAppointmentsForDateDataTable(day);
                dataGridView_appts.DataSource = appts;
            } else {
                // "All Appointments" == selectedValue or anything else if the user tries to type something in..
                DataTable appts = DataAccessLayer.GetAllAppointmentsDataTable();
                dataGridView_appts.DataSource = appts;
            }
        }

        private void DateTimePicker_appts_ValueChanged(object sender, EventArgs e) {
            populateSchedulesGrid();
        }

        private void DataGridView_customers_SelectionChanged(object sender, EventArgs e) {
            if (dataGridView_customers.SelectedRows.Count > 0) {
                var selectedRow = dataGridView_customers.SelectedRows[0];
                selectedCustomerId = (int)selectedRow.Cells[0].Value;
            } else {
                selectedCustomerId = null;
            }
            initNewApptBtn();
        }

        private void initNewApptBtn() {
            if (selectedCustomerId != null) {
                button_appt.Visible = true;
                label_appointments.Text = "Appointments | Click button to make appt. with selected customer →";
            } else {
                button_appt.Visible = false;
                label_appointments.Text = "Appointments | Select a customer to make an appt. ↑";
            }
        }

        private void DataGridView_customers_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = dataGridView_customers.Rows[e.RowIndex];
                selectedCustomerId = (int)row.Cells[0].Value;
                CustomerForm customerForm = new CustomerForm(this, selectedCustomerId);
                customerForm.ShowDialog();
            }
        }

        private void DataGridView_appts_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = dataGridView_appts.Rows[e.RowIndex];
                int aptId = (int)row.Cells[0].Value;
                AppointmentForm apptForm = new AppointmentForm(this, aptId, null);
                apptForm.ShowDialog();
            }
        }

        private void button_appt_Click(object sender, EventArgs e) {
            AppointmentForm apptForm = new AppointmentForm(this, null, selectedCustomerId.Value);
            apptForm.ShowDialog();
        }

        private void comboBox_appts_filter_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedValue = comboBox_appts_filter.SelectedItem;
            if ("Chosen Day" == selectedValue) {
                dateTimePicker_appts.Visible = true;
            } else {
                dateTimePicker_appts.Visible = false;
            }
            populateSchedulesGrid();
        }

        private void button_report1_Click(object sender, EventArgs e) {
            DataTable dataTable = DataAccessLayer.GetAppointmentTypesByMonthReport();

            // Lambda to process the data into a string
            Func<DataTable, string> tableProcessor = dt => {
                string rpt = "";
                string current_month = "";
                foreach (DataRow row in dt.Rows) {
                    string row_month = (string)row[1];
                    string row_count = row[0].ToString();
                    string type = (string)row[2];

                    if (current_month != row_month) {
                        if (row_month != "") rpt += "\r\n";
                        rpt += $"{row_month}:\r\n";
                        current_month = row_month;
                    }

                    rpt += $"\tAppointment Type: {type}\t\tNum of Appts: {row_count}\r\n";
                }
                return rpt;
            };

            ReportForm form = new ReportForm("Appointment Types by Month", dataTable, tableProcessor);
            form.ShowDialog();
        }

        private void button_report2_Click(object sender, EventArgs e) {
            DataTable dataTable = DataAccessLayer.GetUserSchedulesReport();

            // Lambda to process the data into a string
            Func<DataTable, string> tableProcessor = dt => {
                string rpt = "";

                string current_day = "";
                string current_user = "";

                foreach (DataRow row in dt.Rows) {
                    string userName = (string)row[0];
                    string customerName = (string)row[1];
                    string type = (string)row[2];
                    DateTime start = (DateTime)row[3];
                    DateTime end = (DateTime)row[4];
                    start = start.ToLocalTime();
                    end = end.ToLocalTime();
                    string day = start.ToString("MMMM dd, yyyy");
                    string start_time = start.ToString("hh:mm tt");
                    string end_time = end.ToString("hh:mm tt");

                    if (current_user != userName) {
                        if (current_user != "") rpt += "\r\n";
                        rpt += $"Schedule for {userName}:\r\n";
                        current_user = userName;
                    }

                    if (current_day != day) {
                        rpt += $"\t{day}:\r\n";
                        current_day = day;
                    }

                    rpt += $"\t\tAppointment with {customerName} ({type})\r\n\t\t{start_time} - {end_time}\r\n";
                }
                return rpt;
            };

            ReportForm form = new ReportForm("User Schedules", dataTable, tableProcessor);
            form.ShowDialog();
        }

        private void button_report3_Click(object sender, EventArgs e) {
            DataTable dataTable = DataAccessLayer.Top10LongestAppointmentsReport();

            // Lambda to process the data into a string
            Func<DataTable, string> tableProcessor = dt => {
                string rpt = "";
                int count = 1;

                foreach (DataRow row in dt.Rows) {


                    string userName = (string)row[0];
                    string customerName = (string)row[1];
                    DateTime start = (DateTime)row[2];
                    string type = (string)row[3];
                    long hours = (long)row[4];
                    long minutes = (long)row[5];

                    start = start.ToLocalTime();
                    string day = start.ToString("MMMM dd, yyyy");

                    rpt += $"{count}. ";
                    if (hours > 0) rpt += $"{hours} hour";
                    if (hours > 1) rpt += "s";
                    if (hours > 0 && minutes > 0) rpt += " and ";
                    if (minutes > 0) rpt += $"{minutes} minute";
                    if (minutes > 1) rpt += "s";
                    rpt += $":\r\n\t{userName}'s {type} appointment with {customerName} on {day}\r\n\r\n";

                    count++;
                }
                return rpt;
            };

            ReportForm form = new ReportForm($"Top {dataTable.Rows.Count} Longest Appointments", dataTable, tableProcessor);
            form.ShowDialog();
        }
    }
}
