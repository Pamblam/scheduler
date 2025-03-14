using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler {
    public partial class MainScreen : Form {
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
        }

        private void button_add_customer_Click(object sender, EventArgs e) {
            CustomerForm customerForm = new CustomerForm(this, null);
            customerForm.ShowDialog();
        }
    }
}
