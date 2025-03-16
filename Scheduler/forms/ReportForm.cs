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
    public partial class ReportForm : Form {
        public ReportForm(string title, DataTable dt, Func<DataTable, string> tableProcessor) {
            InitializeComponent();
            label_report.Text = $"Report: {title}";
            textBox_report.Text = tableProcessor(dt);
        }

        private void button_close_Click(object sender, EventArgs e) {
            Close();
        }
    }

    
}
