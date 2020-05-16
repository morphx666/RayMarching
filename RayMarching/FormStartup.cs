using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayMarching {
    public partial class FormStartup : Form {
        public FormStartup() {
            InitializeComponent();
        }

        private void ButtonRun_Click(object sender, EventArgs e) {
            Form fs = null;
            this.Hide();
            if(RadioButton1.Checked) fs = new FormStep1();
            if(RadioButton2.Checked) fs = new FormStep2();
            if(RadioButton3.Checked) fs = new FormStep3();
            if(RadioButton4.Checked) fs = new FormStep4();
            fs.Show();
            fs.FormClosed += (_, __) => this.Show();
        }
    }
}
