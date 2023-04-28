using MorphxLibs;
using RayMarching.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayMarching {
    public partial class FormStep1 : Form {
        private readonly List<Shape> shapes = new List<Shape>();
        private readonly Vector camera;

        private bool isMouseDown = false;

        public string Info { get; private set; }

        public FormStep1(string info) {
            InitializeComponent();

            Info = info;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            camera = Vector.FromOrigin(50, this.DisplayRectangle.Height / 2);
            shapes.Add(new Circle(Vector.FromOrigin(800, 500), 50, Color.DimGray));
            shapes.Add(new Box(Vector.FromOrigin(600, 250), 100, 120, Color.DimGray));

            Task.Run(() => {
                while(true) {
                    Thread.Sleep(30);
                    this.Invalidate();
                }
            });

            this.MouseDown += (object s, MouseEventArgs e) => {
                isMouseDown = true;
                camera.TranslateAbs(e.Location.X, e.Location.Y);
            };

            this.MouseMove += (object s, MouseEventArgs e) => {
                if(isMouseDown) {
                    camera.TranslateAbs(e.Location.X, e.Location.Y);
                }
            };

            this.MouseUp += (_, __) => isMouseDown = false;
            Info = info;
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            double distance = double.MaxValue;

            foreach(Shape s in shapes) {
                distance = Math.Min(s.DistanceFrom(camera), distance);

                using(SolidBrush sb = new SolidBrush(s.Color)) {
                    switch(s.Type) {
                        case "Circle":
                            double r = ((Circle)s).Radius;
                            double d = 2 * r;
                            g.FillEllipse(sb, (float)(s.Position.X1 - r), (float)(s.Position.Y1 - r), (float)d, (float)d);
                            break;
                        case "Box":
                            double w = ((Box)s).Width;
                            double h = ((Box)s).Height;
                            g.FillRectangle(sb, (float)(s.Position.X1 - w / 2), (float)(s.Position.Y1 - h / 2), (float)w, (float)h);
                            break;
                    }
                }
            }

            if(distance > 0)
                g.FillEllipse(Brushes.SlateBlue, (float)(camera.X1 - distance),
                                                 (float)(camera.Y1 - distance),
                                                 (float)(2 * distance),
                                                 (float)(2 * distance));
            g.FillEllipse(Brushes.DeepSkyBlue, (float)(camera.X1 - 8),
                                               (float)(camera.Y1 - 8),
                                               16,
                                               16);

            ShowInfo(g);
        }

        private void ShowInfo(Graphics g) {
            g.DrawString(Info, this.Font, Brushes.CadetBlue, 10, 10);
            g.DrawString("Use mouse to drag camera", this.Font, Brushes.WhiteSmoke, 10, this.Font.Height + 10);
        }
    }
}