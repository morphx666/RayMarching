using MorphxLibs;
using RayMarching.Shapes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RayMarching {
    public partial class FormStep4 : Form {
        private readonly List<Shape> shapes = new List<Shape>();
        private readonly Vector camera;
        private readonly double fov = 60 * Constants.ToRad;

        private bool isMouseDown = false;

        private readonly BlockingCollection<Vector> hitPoints = new BlockingCollection<Vector>();
        private readonly AutoResetEvent ae = new AutoResetEvent(false);

        private FormStep4_3DRender form3D;

        private bool isClosing = false;

        public string Info { get; private set; }

        public FormStep4(string info) {
            InitializeComponent();

            Info = info;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            camera = Vector.FromOrigin(50, this.DisplayRectangle.Height / 2);
            camera.Magnitude = 0;

            Color c = Color.FromArgb(84, 44, 64);
            shapes.Add(new Circle(Vector.FromOrigin(200, 500), 80, c));
            shapes.Add(new Box(Vector.FromOrigin(300, 400), 200, 80, c));
            shapes.Add(new Circle(Vector.FromOrigin(300, 200), 40, c));
            shapes.Add(new Circle(Vector.FromOrigin(700, 450), 50, c));
            shapes.Add(new Box(Vector.FromOrigin(600, 250), 100, 120, c));

            SetBackgroundTasks();
            Start3DForm();
            SetEventHandlers();
        }

        private void SetBackgroundTasks() {
            Task.Run(() => {
                while(true) {
                    Thread.Sleep(30);
                    this.Invalidate();
                }
            });

            Task.Run(() => {
                while(true) {
                    GenerateHitPoints();
                    ae.WaitOne();
                }
            });
        }

        private void SetEventHandlers() {
            this.KeyDown += (object s, KeyEventArgs e) => {
                switch(e.KeyCode) {
                    case Keys.Up:
                        camera.Move(3);
                        ae.Set();
                        break;
                    case Keys.Down:
                        camera.Move(-3);
                        ae.Set();
                        break;
                    case Keys.Left:
                        camera.Angle -= 0.8 * Constants.ToRad;
                        ae.Set();
                        break;
                    case Keys.Right:
                        camera.Angle += 0.8 * Constants.ToRad;
                        ae.Set();
                        break;
                }
            };

            this.MouseDown += (object s, MouseEventArgs e) => {
                isMouseDown = true;

                camera.TranslateAbs(e.Location.X, e.Location.Y);
                ae.Set();
            };

            this.MouseMove += (object s, MouseEventArgs e) => {
                if(isMouseDown) {
                    camera.TranslateAbs(e.Location.X, e.Location.Y);
                    ae.Set();
                }
            };

            this.MouseUp += (_, __) => isMouseDown = false;

            this.FormClosing += (_, __) => {
                if(!isClosing) {
                    isClosing = true;
                    form3D.Close();
                }
            };

            form3D.FormClosing += (_, __) => {
                if(!isClosing) {
                    isClosing = true;
                    this.Close();
                }
            };
        }

        private void Start3DForm() {
            form3D = new FormStep4_3DRender();
            form3D.SetData(camera, hitPoints);
            form3D.Show(this);

            if(Screen.AllScreens.Count() > 1) {
                Rectangle r = Screen.AllScreens[1].Bounds;
                form3D.Location = new Point(r.X + (r.Width - form3D.Width) / 2,
                                           r.Y + (r.Height - form3D.Height) / 2);
            }
        }

        private double DistanceToNearestShape(Vector fromVector) {
            double distance = double.MaxValue;

            //double Mix(double x, double y, double a) => x * (1 - a) + y * a;

            //double SMinPoly(double a, double b) {
            //    const double k = 0.1;
            //    double v = 0.5 + 0.5 * (b - a) / k;
            //    double h  = v < 0 ? 0 : v > 1 ? 1 : v;
            //    return Mix(b, a, h) - k * h * (1 - h);
            //};

            foreach(Shape s in shapes)
                distance = Math.Min(s.DistanceFrom(fromVector), distance);
            //distance = SMinPoly(s.DistanceFrom(fromVector), distance);

            return distance;
        }

        private void GenerateHitPoints() {
            Vector cam = new Vector(camera);
            double fromAngle = cam.Angle - fov / 2;
            double toAngle = cam.Angle + fov / 2;
            double angleStep = 0.2 * Constants.ToRad * Math.Sign(toAngle - fromAngle);
            List<Vector> tmpHitPoints = new List<Vector>();

            for(cam.Angle = fromAngle; cam.Angle < toAngle; cam.Angle += angleStep)
                DoWalk(cam, tmpHitPoints);

            while(hitPoints.Count > 0) hitPoints.TryTake(out _);
            foreach(Vector p in tmpHitPoints) hitPoints.TryAdd(p);
        }

        private void DoWalk(Vector fromVector, List<Vector> points) {
            List<Vector> walkingCircles = new List<Vector>();
            Vector walkingCircle = new Vector(fromVector);

            while(true) { // FIXME: I think this can be simplified...
                double distance = DistanceToNearestShape(walkingCircle);
                if(distance <= 0.01) break;

                Vector newWalkingCircle = new Vector(2 * distance, fromVector.Angle, walkingCircle.Origin);
                if(newWalkingCircle.X1 < 0 || newWalkingCircle.X1 > this.DisplayRectangle.Width ||
                   newWalkingCircle.Y1 < 0 || newWalkingCircle.Y1 > this.DisplayRectangle.Height) break;

                walkingCircles.Add(newWalkingCircle);
                walkingCircle.Move(distance);
            }

            if(walkingCircles.Count > 0) {
                Vector lastWalkingCircle = new Vector(walkingCircles[walkingCircles.Count - 1]);
                lastWalkingCircle.Move(lastWalkingCircle.Magnitude / 2);

                if(lastWalkingCircle.X2 > 0 && lastWalkingCircle.X2 < this.DisplayRectangle.Width &&
                   lastWalkingCircle.Y2 > 0 && lastWalkingCircle.Y2 < this.DisplayRectangle.Height) {
                    Vector hit = new Vector(lastWalkingCircle - fromVector) { Origin = lastWalkingCircle.Origin };
                    //if(!points.Contains(hit)) points.Add(hit);
                    points.Add(hit);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            foreach(Shape s in shapes) {
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

            camera.Magnitude = 200;
            camera.Paint(g, Pens.LightBlue);

            double cameraAngle = camera.Angle;
            camera.Angle -= fov / 2;
            camera.Paint(g, Pens.LightCoral);
            camera.Angle += fov;
            camera.Paint(g, Pens.LightCoral);

            camera.Angle = cameraAngle;
            camera.Magnitude = 0;

            foreach(Vector p in hitPoints)
                g.FillEllipse(Brushes.GreenYellow, (float)(p.X1 - 2), (float)(p.Y1 - 2), 4, 4);

            g.FillEllipse(Brushes.DeepSkyBlue, (float)(camera.X1 - 8),
                                               (float)(camera.Y1 - 8),
                                               16,
                                               16);

            ShowInfo(g);
        }

        private void ShowInfo(Graphics g) {
            g.DrawString(Info, this.Font, Brushes.CadetBlue, 10, 10);
            g.DrawString("Use mouse to drag camera", this.Font, Brushes.WhiteSmoke, 10, this.Font.Height + 10);
            g.DrawString("Use arrow keys to move and rotate camera", this.Font, Brushes.WhiteSmoke, 10, this.Font.Height * 2 + 10);
        }
    }
}