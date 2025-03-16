using System.Diagnostics;

namespace Scheduler{
    public partial class Form_login : Form {
        private MainScreen mainScreen;

        public Form_login(MainScreen mainScreen) {
            InitializeComponent();
            this.mainScreen = mainScreen;
        }

        private void button_login_Click(object sender, EventArgs e) {

            bool loginSuccess = DataAccessLayer.Login(
                textBox_username.Text,
                textBox_password.Text
            );

            if (loginSuccess) {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(exeDirectory, "Login_History.txt");
                File.AppendAllText(filePath, $"{DataAccessLayer.User.userName} {DateTime.Now.ToString()}\n");

                Debug.WriteLine($"Logged login to: {filePath}");

                this.Close();
                mainScreen.populateWindow();
                mainScreen.CheckForUpcomingAppts();
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
