using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util.Geometry
{
    [Serializable]
    public class Bounds : IEquatable<Bounds>
    {
        private Vector3 center, size;
        public Vector3 Center
        {
            get { return center; }
        }
        public Vector3 Size
        {
            get { return size; }
        }

        public Bounds()
        {
        }

        public Bounds(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.size = size;
        }

        public bool Equals(Bounds other)
        {
            return center.Equals(other.center) && size.Equals(other.size);
        }
    }
}
