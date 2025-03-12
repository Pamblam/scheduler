using System.Diagnostics;

namespace Scheduler{
    public partial class Form_login : Form {
        public Form_login() {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e) {

            bool loginSuccess = DataAccessLayer.Login(
                textBox_username.Text,
                textBox_password.Text
            );

            if (loginSuccess) {
                Debug.WriteLine("Login success!");
            } else {
                MessageBox.Show(
                    Properties.Resources.login_error_msg,
                    Properties.Resources.error_label,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            
        }
    }
}
