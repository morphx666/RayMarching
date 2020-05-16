using MorphxLibs;
using System.Drawing;

namespace RayMarching.Shapes {
    public class Circle : Shape {
        public double Radius { get; set; }

        public Circle(Vector position, double radius, Color color) : base(position, color) {
            Radius = radius;
        }

        public override double DistanceFrom(Vector p) {
            return (Position - p).Magnitude - Radius;
        }
    }
}
