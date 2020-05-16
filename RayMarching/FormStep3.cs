﻿using MorphxLibs;
using RayMarching.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RayMarching {
    public partial class FormStep3 : Form {
        private List<Shape> shapes = new List<Shape>();
        private List<Vector> walkingCircles = new List<Vector>();
        private Vector camera;

        private bool isMouseDown = false;
        private Point mousePosition;
        private bool swipe = false;
        private List<PointD> hitPoints = new List<PointD>();

        public FormStep3() {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            camera = Vector.FromOrigin(50, this.DisplayRectangle.Height / 2);
            camera.Magnitude = 0;

            Color c = Color.FromArgb(84, 44, 64);
            shapes.Add(new Circle(Vector.FromOrigin(200, 500), 80, c));
            shapes.Add(new Circle(Vector.FromOrigin(300, 200), 40, c));
            shapes.Add(new Circle(Vector.FromOrigin(700, 450), 50, c));
            shapes.Add(new Box(Vector.FromOrigin(600, 250), 100, 120, c));

            Task.Run(() => {
                while(true) {
                    Thread.Sleep(30);
                    this.Invalidate();
                }
            });

            this.KeyDown += (object s, KeyEventArgs e) => {
                switch(e.KeyCode) {
                    case Keys.Up:
                        camera.Move(3);
                        break;
                    case Keys.Down:
                        camera.Move(-3);
                        break;
                    case Keys.Left:
                        camera.Angle -= 0.8 * Constants.ToRad;
                        break;
                    case Keys.Right:
                        camera.Angle += 0.8 * Constants.ToRad;
                        break;
                    case Keys.Enter:
                        camera.Angle = 0;
                        hitPoints.Clear();
                        swipe = !swipe;
                        break;
                }
            };

            this.MouseDown += (object s, MouseEventArgs e) => {
                isMouseDown = true;
                mousePosition = e.Location;
            };

            this.MouseMove += (object s, MouseEventArgs e) => {
                if(isMouseDown) {
                    camera.TranslateAbs(e.Location.X, e.Location.Y);
                }
            };

            this.MouseUp += (_, __) => isMouseDown = false;
        }

        private double DistanceToNearestShape(Vector fromVector) {
            double distance = double.MaxValue;

            foreach(Shape s in shapes) {
                distance = Math.Min(s.DistanceFrom(fromVector), distance);
            }
            return distance;
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            if(swipe) camera.Angle -= 0.2 * Constants.ToRad;

            walkingCircles.Clear();
            Vector walkingCircle = new Vector(camera);
            while(true) { // FIXME: I think this can be simplified...
                double distance = DistanceToNearestShape(walkingCircle);
                if(distance <= 0.01) break;

                Vector newWalkingCircle = new Vector(2 * distance, camera.Angle, walkingCircle.Origin);
                if(newWalkingCircle.X1 < 0 || newWalkingCircle.X1 > this.DisplayRectangle.Width ||
                   newWalkingCircle.Y1 < 0 || newWalkingCircle.Y1 > this.DisplayRectangle.Height) break;

                walkingCircle.Move(distance);
                walkingCircles.Add(newWalkingCircle);
            }

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

            using(SolidBrush sb1 = new SolidBrush(Color.FromArgb(32, 88, 88, 88))) {
                using(SolidBrush sb2 = new SolidBrush(Color.FromArgb(220, 99, 99, 120))) {
                    foreach(Vector wc in walkingCircles) {
                        RectangleF r = new RectangleF((float)(wc.X1 - wc.Magnitude / 2), (float)(wc.Y1 - wc.Magnitude / 2),
                                                      (float)wc.Magnitude, (float)wc.Magnitude);
                        g.FillEllipse(sb1, r);
                        g.FillEllipse(sb2, (float)(wc.X1 - 4), (float)(wc.Y1 - 4), 8, 8);
                    }
                }
            }
            using(Pen p = new Pen(Color.FromArgb(128, 88, 88, 88), 2)) {
                foreach(Vector wc in walkingCircles) {
                    RectangleF r = new RectangleF((float)(wc.X1 - wc.Magnitude / 2), (float)(wc.Y1 - wc.Magnitude / 2),
                                                  (float)wc.Magnitude, (float)wc.Magnitude);
                    try {
                        g.DrawEllipse(p, r);
                    } catch { }
                }
            }

            if(walkingCircles.Count > 0) {
                Vector lastWalkingCircle = walkingCircles[walkingCircles.Count - 1];
                lastWalkingCircle.Move(lastWalkingCircle.Magnitude / 2);
                camera.Magnitude = new Vector(camera.Origin, walkingCircles.Last().Origin).Magnitude;
                camera.Paint(g, Pens.LightBlue);
                camera.Magnitude = 0;

                if(swipe) {
                    if(lastWalkingCircle.X2 > 0 && lastWalkingCircle.X2 < this.DisplayRectangle.Width &&
                       lastWalkingCircle.Y2 > 0 && lastWalkingCircle.Y2 < this.DisplayRectangle.Height) {
                        hitPoints.Add(lastWalkingCircle.Origin);
                    }
                }

                g.FillEllipse(Brushes.Chartreuse, (float)(lastWalkingCircle.X1 - 4),
                                                  (float)(lastWalkingCircle.Y1 - 4),
                                                  8,
                                                  8);
            }

            foreach(PointD p in hitPoints) {
                g.FillEllipse(Brushes.GreenYellow, (float)(p.X - 2), (float)(p.Y - 2), 4, 4);
            }

            g.FillEllipse(Brushes.DeepSkyBlue, (float)(camera.X1 - 8),
                                               (float)(camera.Y1 - 8),
                                               16,
                                               16);

            ShowInfo(g);
        }

        private void ShowInfo(Graphics g) {
            g.DrawString("Use mouse to drag camera", this.Font, Brushes.WhiteSmoke, 10, 10);
            g.DrawString("Use arrow keys to move and rotate camera", this.Font, Brushes.WhiteSmoke, 10, 10 + this.Font.Height);
            g.DrawString("Press ENTER to start swiping the scene and detect visible points", this.Font, Brushes.WhiteSmoke, 10, 10 + this.Font.Height*2);
        }
    }
}