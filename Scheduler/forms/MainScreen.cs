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

namespace Scheduler {
    public partial class MainScreen : Form {
        int? selectedCustomerId = null;

        public MainScreen() {
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
    }
}
