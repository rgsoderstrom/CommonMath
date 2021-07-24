using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath
{
    //******************************************************************************
    //
    //  Vector4D class
    //

    public struct Vector4D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Vector4D (double x, double y, double z, double w)
            : this ()
        {
            X = x; Y = y; Z = z; W = w;
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

        //****************************************************************************
        //
        // ToString ()
        //
        public override String ToString ()
        {
            return string.Format ("[{0:0.000}, {1:0.000}, {2:0.000}, {3:0.000}]", X, Y, Z, W);
        }


        /***



                public double Length {get {return Math.Sqrt (X * X + Y * Y + Z * Z);}}

                //****************************************************************************
                //
                // Constructors
                //

                // passed a string of the form [11 22 33]
                public Vector3D (string values) 
                    : this ()
                {
                    string[] tokens = values.Split (new char[] { '[', ']', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    X = double.Parse (tokens[0]);
                    Y = double.Parse (tokens[1]);
                    Z = double.Parse (tokens[2]);
                }


                public Vector3D (Vector3D src)
                    : this ()
                {
                    X = src.X; Y = src.Y; Z = src.Z;
                }

                public Vector3D (Point3D src)
                    : this ()
                {
                    X = src.X; Y = src.Y; Z = src.Z;
                }

                //****************************************************************************
                //
                // operators
                //

                public static Vector3D operator + (Vector3D v1, Vector3D v2)
                {
                    return new Vector3D (v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
                }

                public static Vector3D operator - (Vector3D v1, Vector3D v2)
                {
                    return new Vector3D (v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
                }

                public static Vector3D operator * (Vector3D v, double s)
                {
                    return new Vector3D (v.X * s, v.Y * s, v.Z * s);
                }

                public static Vector3D operator * (double s, Vector3D v)
                {
                    return new Vector3D (v.X * s, v.Y * s, v.Z * s);
                }

                public static Vector3D operator / (Vector3D v, double s)
                {
                    return new Vector3D (v.X / s, v.Y / s, v.Z / s);
                }

                //****************************************************************************

                public void Normalize ()
                {
                    double r = Length;

                    if (r > 1e-6)
                    {
                        X /= r;
                        Y /= r;
                        Z /= r;
                    }
                    else
                    {
                        Exception ex = new Exception ("Attempt to normalize zero vector");
                        throw ex;
                    }
                }

                public static double Dot (Vector3D v1, Vector3D v2)
                {
                    return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
                }

                public static Vector3D Cross (Vector3D v1, Vector3D v2)
                {
                    Vector3D results = new Vector3D (v1.Y * v2.Z - v1.Z * v2.Y,
                                                    (v1.X * v2.Z - v1.Z * v2.X) * -1,
                                                     v1.X * v2.Y - v1.Y * v2.X);
                    return results;
                }


                //****************************************************************************
                //
                // ToString ()
                //

                public override String ToString ()
                {
                    return string.Format ("[{0}, {1}, {2}]", X, Y, Z);
                }
         ****/
    }
}
