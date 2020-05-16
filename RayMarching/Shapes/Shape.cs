using MorphxLibs;
using System.Drawing;
using System.Linq;

namespace RayMarching.Shapes {
    public abstract class Shape {
        public Vector Position { get; set; }
        public Color Color { get; set; }

        public string Type { get; private set; }

        protected Shape(Vector position, Color color) {
            Position = position;
            Color = color;
            Type = this.GetType().FullName.Split('.').Last();
        }

        public abstract double DistanceFrom(Vector p);
    }
}
