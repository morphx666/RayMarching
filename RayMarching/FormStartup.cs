using System;
using System.Windows.Forms;

// Code inspired by Sebastian Lague's video: https://www.youtube.com/watch?v=Cp5WWtMoeKg

// https://iquilezles.org/www/articles/distfunctions/distfunctions.htm
// http://jamie-wong.com/2016/07/15/ray-marching-signed-distance-functions/#the-raymarching-algorithm
// https://www.iquilezles.org/www/articles/smin/smin.htm
// http://blog.hvidtfeldts.net/index.php/2011/09/distance-estimated-3d-fractals-v-the-mandelbulb-different-de-approximations/

namespace RayMarching {
    public partial class FormStartup : Form {
        public FormStartup() {
            InitializeComponent();
        }

        private void ButtonRun_Click(object sender, EventArgs e) {
            Form fs = null;
            this.Hide();
            if(RadioButton1.Checked) fs = new FormStep1(LabelStep1.Text);
            if(RadioButton2.Checked) fs = new FormStep2(LabelStep2.Text);
            if(RadioButton3.Checked) fs = new FormStep3(LabelStep3.Text);
            if(RadioButton4.Checked) fs = new FormStep4(LabelStep4.Text);
            fs.Show();
            fs.FormClosed += (_, __) => this.Show();
        }
    }
}
