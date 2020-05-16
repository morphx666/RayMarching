using MorphxLibs;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayMarching {
    public partial class FormStep4_3DRender : Form {
        private BlockingCollection<Vector> hitPoints = new BlockingCollection<Vector>();
        private Vector camera = Vector.Empty;

        public FormStep4_3DRender() {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            Task.Run(() => {
                while(true) {
                    Thread.Sleep(30);
                    this.Invalidate();
                }
            });
        }

        public void SetData(Vector camera, BlockingCollection<Vector> points) {
            this.camera = camera;
            hitPoints = points;
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            double x;
            double y;
            double p;
            int a;
            double fov = 60 * Constants.ToRad;
            double viewDistance = (this.DisplayRectangle.Width / 2.0) / Math.Tan(fov / 2.0);
            double rw;
            double fromAngle = camera.Angle - fov / 2;
            double toAngle = camera.Angle + fov / 2;
            double angleStep = 0.2 * Constants.ToRad * Math.Sign(toAngle - fromAngle);

            double GetX(double angle) => (angle - fromAngle) / (toAngle - fromAngle) * this.DisplayRectangle.Width;

            foreach(Vector h in hitPoints) {
                p = (h.X2 - h.X1) * h.AngleCos + // https://youtu.be/eOCQfxRQ2pY?t=606
                    (h.Y2 - h.Y1) * h.AngleSin;

                x = GetX(h.Angle);
                rw = GetX(h.Angle + angleStep) - x;
                y = Math.Min((this.DisplayRectangle.Height / 28.0) * viewDistance / p, this.DisplayRectangle.Height);
                a = Math.Max(Math.Min((int)(2000_000.0 / (p * p)), 255), 0);

                using(SolidBrush b = new SolidBrush(Color.FromArgb(a, Color.LightGray))) {
                    g.FillRectangle(b, (float)x, (float)((this.DisplayRectangle.Height - y) / 2.0), (float)rw, (float)y);
                }
            }
        }
    }
}