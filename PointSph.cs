using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

//
// PointRTP - Spherical coordiantes point
//          - rho, theta, phi
//
namespace CommonMath
{
    public class PointSph : ICartesian
    {
        // private backing store
        private double rho = 0;
        private double thetaRad = 0; // radians, 0 - 2 * PI
        private double phiRad = 0;   // radians, 0 - PI

        // ICartesian 
        public double Rho   
        {
            get {return rho;}
            set {rho = value;}
        }

        public double Theta  // angle in degrees in XY plane from +X axis, 0 - 360       
        {
            get {return thetaRad * 180 / Math.PI;}
            set {thetaRad = value * Math.PI / 180;}
        }

        public double Phi // angle in degrees from +Z axis, 0 - 180
        {
            get {return phiRad * 180 / Math.PI;}
            set {phiRad = value * Math.PI / 180;}
        } 

        public double X 
        {
            get {return rho * Math.Sin (phiRad) * Math.Cos (thetaRad);}
            set {Update (value, Y, Z);}
        }

        public double Y 
        {
            get {return rho * Math.Sin (phiRad) * Math.Sin (thetaRad); }
            set {Update (X, value, Z);}
        }

        public double Z
        {
            get {return rho * Math.Cos (phiRad);}
            set {Update (X, Y, value);}
        }

        public Point3D Point3D
        {
            get {return new Point3D (X, Y, Z); }
            set {X = value.X; Y = value.Y; Z = value.Z;}
        }

        //
        // Constructors
        //

        /// <summary>
        /// Default constructor
        /// </summary>
        public PointSph () : this (0, 0, 0)
        {
        }

        /// <summary>
        /// from (rho, theta, phi), angles in degrees
        /// </summary>
        public PointSph (double r, double t, double p)
        {
            rho      = r;
            thetaRad = t * Math.PI / 180;
            phiRad   = p * Math.PI / 180;
        }

        /// <summary>
        /// from CommonMath PointXYZ, PointSph or PointCyl
        /// </summary>       
        public PointSph (ICartesian pt)
        {
            Update (pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// from Windows Point3D
        /// </summary>       
        public PointSph (Point3D src)
        {
            Update (src.X, src.Y, src.Z);
        }

        //
        // private helper function to update backing store
        //
        private void Update (double x, double y, double z)
        {
            rho      = Math.Sqrt  (x*x + y*y + z*z);
            thetaRad = Math.Atan2 (y, x);
            phiRad   = Math.Atan2 (Math.Sqrt (x*x + y*y), z);  
        }

        //
        // ToString
        //
        public override string ToString ()
        {
            return string.Format ("rho: {0:0.000}, theta: {1:0.000}, phi: {2:0.000}", Rho, Theta, Phi);
        }
    }
}
