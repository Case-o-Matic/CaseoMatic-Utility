using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    [Serializable]
    public struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 zero = new Vector3();

        public double x, y, z;
        public double Length
        {
            get
            {
                return Math.Sqrt((x * 2) + (y * 2) + (z * 2));
            }
        }

        public Vector3(double x = 0, double y = 0, double z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public UnityEngine.Vector3 ToUnityVector()
        {
            return new UnityEngine.Vector3((float)x, (float)y, (float)z);
        }

        public bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override bool Equals(object obj)
        {
            return Equals((Vector3)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static double Distance(Vector3 one, Vector3 two)
        {
            var dir = two - one;
            return dir.Length;
        }

        #region Operators
        public static Vector3 operator +(Vector3 one, Vector3 two)
        {
            one.x += two.x;
            one.y += two.y;
            one.z += two.z;

            return one;
        }
        public static Vector3 operator -(Vector3 one, Vector3 two)
        {
            one.x -= two.x;
            one.y -= two.y;
            one.z -= two.z;

            return one;
        }
        public static Vector3 operator *(Vector3 one, double factor)
        {
            one.x *= factor;
            one.y *= factor;
            one.z *= factor;

            return one;
        }
        public static bool operator ==(Vector3 one, Vector3 two)
        {
            return one.Equals(two);
        }
        public static bool operator !=(Vector3 one, Vector3 two)
        {
            return !one.Equals(two);
        }
        #endregion
    }
}
