using System;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    public class SphericalCoordPoint : ICartesian
    {   
        private double rho;
        private double theta; // angle in XY plane from +X axis, 0 - 2*pi
        private double phi;   // angle from +Z axis, 0 - pi

        public SphericalCoordPoint ()
        {
        }

        public SphericalCoordPoint (double r, double thetaDeg, double phiDeg)
        {
            rho   = r;
            theta = thetaDeg * Math.PI / 180;
            phi   = phiDeg   * Math.PI / 180;
        }

        public SphericalCoordPoint (CartesianCoordPoint src)
        {
            rho   = Math.Sqrt  (src.X*src.X + src.Y*src.Y + src.Z*src.Z);
            theta = Math.Atan2 (src.Y, src.X);
            phi   = Math.Atan2 (Math.Sqrt (src.X*src.X + src.Y*src.Y), src.Z);  
        }

        public SphericalCoordPoint (CartesianCoordPoint src, CartesianCoordPoint origin)
        {
            double dx = src.X - origin.X;
            double dy = src.Y - origin.Y;
            double dz = src.Z - origin.Z;

            rho   = Math.Sqrt  (dx*dx + dy*dy + dz*dz);
            theta = Math.Atan2 (dy, dx);
            phi   = Math.Atan2 (Math.Sqrt (dx*dx + dy*dy), dz);  
        }

        public double Rho {get {return rho;}
                           set {rho = value;}}

        public double ThetaRad {get {return theta;}
                                set {theta = value;}}

        public double ThetaDeg {get {return theta * 180 / Math.PI;}
                                set {theta = value * Math.PI / 180;}}

        public double PhiRad {get {return phi;}
                              set {phi = value;}}

        public double PhiDeg {get {return phi * 180 / Math.PI;}
                              set {phi = value * Math.PI / 180;}}

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
                double z = value.Z;

                rho   = Math.Sqrt  (x*x + y*y + z*z);
                theta = Math.Atan2 (y, x);
                phi   = Math.Atan2 (Math.Sqrt (x*x + y*y), z);  
            }
        }

        public double X
        {
            get 
            {
               return rho * Math.Sin (phi) * Math.Cos (theta);
            }

            set
            {
                double x = value;
                
                rho   = Math.Sqrt  (x*x + Y*Y + Z*Z);
                theta = Math.Atan2 (Y, X);
                phi   = Math.Atan2 (Math.Sqrt (x*x + Y*Y), Z);  
            }
        }

        public double Y
        {
            get 
            {
               return rho * Math.Sin (phi) * Math.Sin (theta);
            }

            set
            {
                double y = value;
                
                rho   = Math.Sqrt  (X*X + y*y + Z*Z);
                theta = Math.Atan2 (y, X);
                phi   = Math.Atan2 (Math.Sqrt (X*X + y*y), Z);  
            }
        }

        public double Z
        {
            get 
            {
               return rho * Math.Cos (phi);
            }

            set
            {
                double z = value;
                
                rho   = Math.Sqrt  (X*X + Y*Y + z*z);
          //    theta = Math.Atan2 (Y, X);
                phi   = Math.Atan2 (Math.Sqrt (X*X + Y*Y), z);  
            }
        }

        public override string ToString ()
        {
            return string.Format ("rho: {0:0.000}, thetaDeg: {1:0.000}, phiDeg: {2:0.000}", rho, ThetaDeg, PhiDeg);
        }
    }
}

