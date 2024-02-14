using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.Text;
using System.Threading.Tasks;

namespace CommonMath
{
    public class PointXYZ : ICartesian
    {
        public double X {get; set;}
        public double Y {get; set;}
        public double Z {get; set;}

        public Point3D Point3D
        {
            get {return new Point3D (X, Y, Z);}
            set {X = value.X; Y = value.Y; Z = value.Z;}
        }
            
        //
        // Constructors
        //

        /// <summary>
        /// Default constuctor
        /// </summary>
        public PointXYZ () : this (0, 0, 0)
        {
        }

        public PointXYZ (double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        // from Windows Point3D
        public PointXYZ (Point3D pt)
        {
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
        }

        /// <summary>
        /// from Windows Vector3D
        /// </summary>
        public PointXYZ (Vector3D src)
        {
            X = src.X;
            Y = src.Y;
            Z = src.Z;
        }

        /// <summary>
        /// from CommonMath PointXYZ, PointSph or PointCyl
        /// </summary>       
        public PointXYZ (ICartesian pt)
        {
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
        }

        //**************************************************************************************

        public static Vector3D operator- (PointXYZ p2, PointXYZ p1)
        {
            return p2.Point3D - p1.Point3D;
        }
        
        public static PointXYZ operator+ (PointXYZ pt, Vector3D v)
        {
            return new PointXYZ (pt.X + v.X, pt.Y + v.Y, pt.Z + v.Z);
        }
              
        //**************************************************************************************
        //
        // ToString
        //
        public override string ToString ()
        {
            return string.Format ("X: {0:0.000}, Y: {1:0.000}, Z: {2:0.000}", X, Y, Z);
        }
    }
}
