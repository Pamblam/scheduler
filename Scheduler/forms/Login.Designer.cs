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
        private void InitializeComponent(){
            label_username = new Label();
            textBox_username = new TextBox();
            label_password = new Label();
            textBox_password = new TextBox();
            button_login = new Button();
            SuspendLayout();
            // 
            // label_username
            // 
            label_username.AutoSize = true;
            label_username.Location = new Point(12, 9);
            label_username.Name = "label_username";
            label_username.Size = new Size(60, 15);
            label_username.TabIndex = 0;
            label_username.Text = "Username";
            label_username.Click += label1_Click;
            // 
            // textBox_username
            // 
            textBox_username.Location = new Point(12, 27);
            textBox_username.Name = "textBox_username";
            textBox_username.Size = new Size(250, 23);
            textBox_username.TabIndex = 1;
            // 
            // label_password
            // 
            label_password.AutoSize = true;
            label_password.Location = new Point(12, 64);
            label_password.Name = "label_password";
            label_password.Size = new Size(57, 15);
            label_password.TabIndex = 2;
            label_password.Text = "Password";
            // 
            // textBox_password
            // 
            textBox_password.Location = new Point(12, 82);
            textBox_password.Name = "textBox_password";
            textBox_password.Size = new Size(250, 23);
            textBox_password.TabIndex = 3;
            textBox_password.UseSystemPasswordChar = true;
            // 
            // button_login
            // 
            button_login.Location = new Point(12, 120);
            button_login.Name = "button_login";
            button_login.Size = new Size(250, 23);
            button_login.TabIndex = 4;
            button_login.Text = "Login";
            button_login.UseVisualStyleBackColor = true;
            // 
            // Form_login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(274, 160);
            Controls.Add(button_login);
            Controls.Add(textBox_password);
            Controls.Add(label_password);
            Controls.Add(textBox_username);
            Controls.Add(label_username);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_login";
            Text = "Scheduler | Login";
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
