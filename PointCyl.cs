using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

//
// PointCyl - Cylindrical coordinates
//          - rho, theta, Z
//
namespace CommonMath
{
    public class PointCyl : ICartesian
    {
        // backing store
        private double rho      = 0;
        private double thetaRad = 0; // radians, 0 - 2 * PI
        private double z        = 0;

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

        public double X
        {
            get {return rho * Math.Cos (thetaRad);}
            set {Update (value, Y, Z);}
        }

        public double Y
        {
            get {return rho * Math.Sin (thetaRad);}
            set {Update (X, value, Z);}
        }

        public double Z
        {
            get {return z;}
            set {Update (X, Y, value);}
        }

        public Point3D Point3D
        {
            get {return new Point3D (X, Y, Z); }
            set {X = value.X; Y = value.Y; Z = value.Z;}
        }

        //*********************************************************************************
        //
        // Constructors
        //

        /// <summary>
        /// Default constructors
        /// </summary>
        public PointCyl () : this (0, 0, 0)
        {
        }

        /// <summary>
        /// from (rho, theta, phi), angles in degrees
        /// </summary>
        public PointCyl (double r, double t, double _z)
        {
            rho      = r;
            thetaRad = t * Math.PI / 180;
            z        = _z;
        }

        // from Windows Point3D
        public PointCyl (Point3D src)
        {
            Update (src.X, src.Y, src.Z);
        }

        /// <summary>
        /// from CommonMath PointXYZ, PointSph or PointCyl
        /// </summary>       
        public PointCyl (ICartesian pt)
        {
            Update (pt.X, pt.Y, pt.Z);
        }

        //
        // private helper function to update backing store
        //
        private void Update (double x, double y, double _z)
        {
            rho      = Math.Sqrt  (x * x + y * y);
            thetaRad = Math.Atan2 (y, x);
            z        = _z;  
        }

        //
        // ToString
        //
        public override string ToString ()
        {
            return string.Format ("rho: {0:0.000}, theta: {1:0.000}, Z: {2:0.000}", Rho, Theta, Z);
        }
    }
}
