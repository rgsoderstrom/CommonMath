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
        /// from CommonMath PointXYZ, PointSph or PointCyl
        /// </summary>       
        public PointXYZ (ICartesian pt)
        {
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
        }

        //
        // ToString
        //
        public override string ToString ()
        {
            return string.Format ("X: {0:0.000}, Y: {1:0.000}, Z: {2:0.000}", X, Y, Z);
        }
    }
}
