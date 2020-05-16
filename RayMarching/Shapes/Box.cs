using MorphxLibs;
using System.Drawing;

namespace RayMarching.Shapes {
    public class Box : Shape {
        private double mWidth;
        private double mHeight;
        private Vector size = Vector.Empty;

        public double Width {
            get => mWidth;
            set {
                mWidth = value;
                SetSizeVector();
            }
        }
        public double Height {
            get => mHeight;
            set {
                mHeight = value;
                SetSizeVector();
            }
        }

        public Box(Vector position, double width, double height, Color color) : base(position, color) {
            Width = width;
            Height = height;
        }

        private void SetSizeVector() {
            size = new Vector(mWidth / 2, mHeight / 2);
        }

        public override double DistanceFrom(Vector p) {
            Vector offset = Vector.Abs(Position - p) - size;

            double unSignedDistance = Vector.Max(offset, Vector.Empty).Magnitude;
            double distanceInsideRect = Vector.Max(Vector.Min(offset, Vector.Empty));
            return unSignedDistance + distanceInsideRect;
        }
    }
}