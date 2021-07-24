using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

//using Common;

namespace CommonMath
{
    //******************************************************************************
    //
    //  Point4DH class - Homogeneous coordinates
    //

    public struct Point4DH
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        //****************************************************************************
        //
        // Constructors
        //
/**
        public Point4DH (Point3D_Spherical src)
            : this ()
        {
            X = src.Rho * Math.Sin (src.Phi) * Math.Cos (src.Theta);
            Y = src.Rho * Math.Sin (src.Phi) * Math.Sin (src.Theta);
            Z = src.Rho * Math.Cos (src.Phi);
            W = 1;
        }
**/
        public Point4DH (Point3D src)
            : this ()
        {
            X = src.X;
            Y = src.Y;
            Z = src.Z;
            W = 1;
        }

        public Point4DH (double a1, double a2, double a3, double a4)
            : this ()
        {
            X = a1; Y = a2; Z = a3; W = a4;
        }

        public Point4DH (double a1, double a2, double a3)
            : this ()
        {
            X = a1; Y = a2; Z = a3; W = 1;
        }

        public Point4DH (Point4DH src)
            : this ()
        {
            X = src.X; Y = src.Y; Z = src.Z; W = src.W;
        }

        //****************************************************************************
        //
        // methods
        //

        public void Normalize ()
        {
            X /= W; Y /= W; Z /= W; W = 1;
        }

        public static Point4DH Normalize (Point4DH p)
        {
            Point4DH r = new Point4DH ();

            r.X /= p.W; r.Y /= p.W; r.Z /= p.W; r.W = 1;

            return r;
        }

        //****************************************************************************
        //
        // operators
        //

        public double this[int a]
        {
            get 
            {
                if (a == 0) return X;
                if (a == 1) return Y;
                if (a == 2) return Z;
                if (a == 3) return W;

                throw (new Exception ("Invalid index, Point4D"));
            }

            set 
            {
                if (a == 0) {X = value; return;}
                if (a == 1) {Y = value; return;}
                if (a == 2) {Z = value; return;}
                if (a == 3) {W = value; return;}

                throw (new Exception ("Invalid index, Point4D"));
            }
        }

        
/*
        public static Point3D operator + (Point3D p, Vector3D v)
        {
            return new Point3D (p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3D operator + (Vector3D v, Point3D p)
        {
            return new Point3D (p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3D operator - (Point3D p, Vector3D v)
        {
            return new Point3D (p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static Vector3D operator - (Point3D p1, Point3D p2)
        {
            return new Vector3D (p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }
*/
        //****************************************************************************
        //
        // ToString ()
        //

        public override String ToString ()
        {
            return string.Format ("[{0:0.000}, {1:0.000}, {2:0.000}, {3:0.000}]", X, Y, Z, W);
        }
    }
}
