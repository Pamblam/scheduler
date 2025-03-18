using System.Diagnostics;

namespace Scheduler{
    public partial class Form_login : Form {
        private MainScreen mainScreen;
        bool loginSuccess = false;

        public Form_login(MainScreen mainScreen) {
            InitializeComponent();
            this.mainScreen = mainScreen;
        }

        private void button_login_Click(object sender, EventArgs e) {

            loginSuccess = DataAccessLayer.Login(
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

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (!loginSuccess) {
                DialogResult result = MessageBox.Show(
                    "You have not logged in. Would you like to close the app?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation
                );
                if (result == DialogResult.Yes) {
                    Environment.Exit(1);
                    return;
                } else {
                    e.Cancel = true;
                }
            }
        }
    }
}
