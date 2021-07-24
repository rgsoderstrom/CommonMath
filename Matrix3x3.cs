using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    [Serializable]
    public class Matrix3x3 : Matrix
    {
          const int N = 3;

        //****************************************************************************
        //
        // Constructors
        //

        public Matrix3x3 () : base (3,3)
        {
        }

        public Matrix3x3 (double a, double b, double c, double d, double e, double f, double g, double h, double i) : base (3,3)
        {
            data [0, 0] = a;
            data [0, 1] = b;
            data [0, 2] = c;
            data [1, 0] = d;
            data [1, 1] = e;
            data [1, 2] = f;
            data [2, 0] = g;
            data [2, 1] = h;
            data [2, 2] = i;
        }

        public Matrix3x3 (Matrix3x3 src) : base (3,3)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    data[i, j] = src.data [i, j];
                }
            }
        }

        //****************************************************************************
        //
        // operators and indexers
        //
/*
        public double this [int row, int col]  // zero based indices
        {
            get {return data [row, col];}
            set {data [row, col] = value;}
        }

        public double Get (int row, int col)  // one based indices
        {
            return this [row-1, col-1];
        }

        public void Set (int row, int col, double value)  // one based indices
        {
            this [row-1, col-1] = value;
        }
        */

        public static Matrix3x3 operator* (Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 results = new Matrix3x3 ();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        results[i, j] += m1.data[i, k] * m2.data[k, j];
                    }
                }
            }

            return results;
        }
/**
        public Matrix3x3 Inverse ()              IN BASE CLASS
        {
            Matrix3x3 results = new Matrix3x3 ();

            double a11 = Get (1,1)            ;
            double a12 = Get (1,2);
            double a13 = Get (1,3);
            double a21 = Get (2,1);
            double a22 = Get (2,2);
            double a23 = Get (2,3);
            double a31 = Get (3,1);
            double a32 = Get (3,2);
            double a33 = Get (3,3);

            double detA = a11 * a22 * a33 + a21 * a32 * a13 + a31 * a12 * a23 - a11 * a32 * a23 - a31 * a22 * a13 - a21 * a12 * a33;

            if (Math.Abs (detA) < 1e-9)
                throw new Exception ("Determinant too small");

            results.Set (1, 1, (a22 * a33 - a23 * a32) / detA);
            results.Set (1, 2, (a13 * a32 - a12 * a33) / detA);
            results.Set (1, 3, (a12 * a23 - a13 * a22) / detA);

            results.Set (2, 1, (a23 * a31 - a21 * a33) / detA);
            results.Set (2, 2, (a11 * a33 - a13 * a31) / detA);
            results.Set (2, 3, (a13 * a21 - a11 * a23) / detA);

            results.Set (3, 1, (a21 * a32 - a22 * a31) / detA);
            results.Set (3, 2, (a12 * a31 - a11 * a32) / detA);
            results.Set (3, 3, (a11 * a22 - a12 * a21) / detA);

            return results;
        } **/

/*
        // assume p1 is a column vector
        public static Point3D operator * (Matrix3x3 m1, Point3D p1)
        {
            Point3D results = new Point3D ();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    results[i] += m1.data[i, j] * p1 [j];                
                }
            }

            return results;
        }
*/
        //****************************************************************************
        //
        // ToString ()
        //

        public override String ToString ()
        {
            return string.Format ("[{0:0.000}, {1:0.000}, {2:0.000}]\n", data[0, 0], data[0, 1], data[0, 2])
                 + string.Format ("[{0:0.000}, {1:0.000}, {2:0.000}]\n", data[1, 0], data[1, 1], data[1, 2])
                 + string.Format ("[{0:0.000}, {1:0.000}, {2:0.000}]\n", data[2, 0], data[2, 1], data[2, 2]);
        }
    }
}
