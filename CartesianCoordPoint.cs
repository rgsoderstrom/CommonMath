using System;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    public class CartesianCoordPoint : ICartesian
    {
        private double x;
        private double y;
        private double z;

        public CartesianCoordPoint (Vector3D src)
        {
            x = src.X;
            y = src.Y;
            z = src.Z;
        }

        public CartesianCoordPoint (Point3D src)
        {
            x = src.X;
            y = src.Y;
            z = src.Z;
        }

        public CartesianCoordPoint (double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public Point3D Cartesian
        {
            get
            {
                return new Point3D (x, y, z);
            }

            set
            {
                x = value.X;
                y = value.Y;
                z = value.Z;
            }
        }

        public double X
        {
            get 
            {
               return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get 
            {
               return y;
            }

            set
            {
                y = value;
            }
        }
        
        public double Z
        {
            get 
            {
               return z;
            }

            set
            {
                z = value;
            }
        }

        public override string ToString ()
        {
            return string.Format ("x:{0:0.000}, y: {1:0.000}, z: {2:0.000}", X, Y, Z);
        }
    }
}
