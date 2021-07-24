using System;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    public class CylindricalCoordPoint : ICartesian
    {
        protected double rho;
        protected double theta; // angle in XY plane from +X axis, 0 - 2*pi
        protected double z;

        public CylindricalCoordPoint ()
        {
        }

        public CylindricalCoordPoint (double r, double thetaDeg, double _z)
        {
            rho   = r;
            theta = thetaDeg * Math.PI / 180;
            z = _z;
        }

        public CylindricalCoordPoint (CartesianCoordPoint src)
        {
            rho   = Math.Sqrt  (src.X*src.X + src.Y*src.Y + src.Z*src.Z);
            theta = Math.Atan2 (src.Y, src.X);
            z     = src.Z;  
        }

        public double Rho {get {return rho;}
                           set {rho = value;}}

        public double ThetaRad {get {return theta;}
                                set {theta = value;}}

        public double ThetaDeg {get {return theta * 180 / Math.PI;}
                                set {theta = value * Math.PI / 180;}}

        public double Z {get {return z;}
                         set {z = value;}}

        public Point3D Cartesian
        {
            get
            {
                return new Point3D (X, Y, Z);
            }

            set
            {
                double x = value.X;
                double y = value.Y;
                z        = value.Z;

                rho   = Math.Sqrt  (x*x + y*y + z*z);
                theta = Math.Atan2 (y, x);
            }
        }

        public double X
        {
            get 
            {
               return rho * Math.Cos (theta);
            }

            set
            {
                double x = value;
                
                rho   = Math.Sqrt  (x*x + Y*Y);
                theta = Math.Atan2 (Y, x);
            }
        }

        public double Y
        {
            get 
            {
               return rho * Math.Sin (theta);
            }

            set
            {
                double y = value;
                
                rho   = Math.Sqrt  (X*X + y*y);
                theta = Math.Atan2 (y, X);
            }
        }

        public override string ToString ()
        {
            return string.Format ("rho: {0:0.000}, thetaDeg: {1:0.000}, z: {2:0.000}", Rho, ThetaDeg, Z);
        }
    }
}
