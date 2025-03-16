namespace Scheduler {
    partial class ReportForm {
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
            label_report = new Label();
            button_close = new Button();
            textBox_report = new TextBox();
            SuspendLayout();
            // 
            // label_report
            // 
            label_report.AutoSize = true;
            label_report.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_report.Location = new Point(12, 9);
            label_report.Name = "label_report";
            label_report.Size = new Size(97, 20);
            label_report.TabIndex = 1;
            label_report.Text = "Report: xxxx";
            // 
            // button_close
            // 
            button_close.Location = new Point(713, 415);
            button_close.Name = "button_close";
            button_close.Size = new Size(75, 23);
            button_close.TabIndex = 2;
            button_close.Text = "Close";
            button_close.UseVisualStyleBackColor = true;
            button_close.Click += this.button_close_Click;
            // 
            // textBox_report
            // 
            textBox_report.Location = new Point(12, 32);
            textBox_report.Multiline = true;
            textBox_report.Name = "textBox_report";
            textBox_report.ReadOnly = true;
            textBox_report.ScrollBars = ScrollBars.Vertical;
            textBox_report.Size = new Size(776, 377);
            textBox_report.TabIndex = 3;
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox_report);
            Controls.Add(button_close);
            Controls.Add(label_report);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReportForm";
            Text = "Report";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label_report;
        private Button button_close;
        private TextBox textBox_report;
    }
}