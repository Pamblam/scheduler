namespace Scheduler{
    partial class Form_login{
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing){
            if (disposing && (components != null)){
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_login));
            label_username = new Label();
            textBox_username = new TextBox();
            label_password = new Label();
            textBox_password = new TextBox();
            button_login = new Button();
            SuspendLayout();
            // 
            // label_username
            // 
            resources.ApplyResources(label_username, "label_username");
            label_username.Name = "label_username";
            // 
            // textBox_username
            // 
            resources.ApplyResources(textBox_username, "textBox_username");
            textBox_username.Name = "textBox_username";
            // 
            // label_password
            // 
            resources.ApplyResources(label_password, "label_password");
            label_password.Name = "label_password";
            // 
            // textBox_password
            // 
            resources.ApplyResources(textBox_password, "textBox_password");
            textBox_password.Name = "textBox_password";
            textBox_password.UseSystemPasswordChar = true;
            // 
            // button_login
            // 
            resources.ApplyResources(button_login, "button_login");
            button_login.Name = "button_login";
            button_login.UseVisualStyleBackColor = true;
            button_login.Click += button_login_Click;
            // 
            // Form_login
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_login);
            Controls.Add(textBox_password);
            Controls.Add(label_password);
            Controls.Add(textBox_username);
            Controls.Add(label_username);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_username;
        private TextBox textBox_username;
        private Label label_password;
        private TextBox textBox_password;
        private Button button_login;
    }
}
