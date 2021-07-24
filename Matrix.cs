using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMath
{
    public partial class Matrix
    {
        protected double[,] data;

        public int Rows {get {return data.GetLength (0);}}
        public int Cols {get {return data.GetLength (1);}}
        
        //****************************************************************************
        //
        // Constructors
        //

        public Matrix (int rows, int cols)
        {
            data = new double [rows, cols];
        }

        public bool IsScalar       {get {return Rows == 1 && Cols == 1;}}
        public bool IsRowVector    {get {return Rows == 1 && Cols >  1;}}
        public bool IsColumnVector {get {return Rows >  1 && Cols == 1;}}

        public double AsScalar
        {
            get
            {
                if (IsScalar)
                    return data [0, 0];
                else
                    throw new Exception ("Operand is not a scalar");
            }
        }

        //****************************************************************************
        //
        // fill with data
        //
        //            double [] input = new double [] {
        //                                  0.3759,
        //                                  0.0183,
        //                                  0.9134,
        //                                  0.3580};
        //
        //            A.FillByRow (input);
        //

        public void FillByRow (double[] src)
        {
            if (Rows * Cols != src.GetLength (0))
                throw new Exception ("Size error in FillByRow");

            int get = 0;

            for (int i=0; i<Rows; i++)
                for (int j=0; j<Cols; j++)
                    data [i,j] = src [get++];
        }

        public void FillByColumn (double[] src)
        {
            if (Rows * Cols != src.GetLength (0))
                throw new Exception ("Size error in FillByColumn");

            int get = 0;

            for (int i=0; i<Cols; i++)
                for (int j=0; j<Rows; j++)
                    data [j,i] = src [get++];
        }

        public void FillOneColumn (int col, List<double> values)
        {
            if (Rows != values.Count)
                throw new Exception ("FillOneColumn: number matrix rows must equal length of values list");

            for (int i=0; i<Rows; i++)
                data [i, col] = values [i];
        }

        //****************************************************************************
        //
        // operators and indexers
        //

        public double this [int row, int col]  // zero-based indices
        {
            get {return data [row, col];}
            set {data [row, col] = value;}
        }

        public double Get (int row, int col)  // one-based indices
        {
            return this [row-1, col-1];
        }

        public void Set (int row, int col, double value)  // one-based indices
        {
            this [row-1, col-1] = value;
        }



        //*************************************************************************************
        //
        // Addition
        //
        public static Matrix operator+ (Matrix m1, Matrix m2)
        {
            if (m1.Rows == 1 && m1.Cols == 1)
            {
                return m1 [0, 0] + m2;
            }

            else if (m2.Rows == 1 && m2.Cols == 1)
            {
                return m1 + m2 [0, 0];
            }

            else
            {
                if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                    throw new Exception ("Matrix addition size error");

                Matrix results = new Matrix (m1.Rows, m1.Cols);

                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        results [i, j] = m1.data [i, j] + m2.data [i, j];
                    }
                }

                return results;
            }
        }

        public static Matrix operator+ (Matrix m1, double s2)
        {
            return s2 + m1;
        }
        
        public static Matrix operator+ (double s1, Matrix m2)
        {
            Matrix results = new Matrix (m2.Rows, m2.Cols);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results[i, j] = s1 + m2.data[i, j];
                }
            }

            return results;
        }

        //*************************************************************************************
        //
        // Subtraction 
        //
        public static Matrix operator- (Matrix m1, Matrix m2)
        {
            if (m1.Rows == 1 && m1.Cols == 1)
            {
                return m1 [0, 0] - m2;
            }

            else if (m2.Rows == 1 && m2.Cols == 1)
            {
                return m1 - m2 [0, 0];
            }

            else
            {
                if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                    throw new Exception ("Matrix subtraction size error");

                Matrix results = new Matrix (m1.Rows, m1.Cols);

                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        results [i, j] = m1.data [i, j] - m2.data [i, j];
                    }
                }

                return results;
            }
        }

        public static Matrix operator- (Matrix m1, double s2)
        {
            return -1 * (s2 - m1);
        }
        
        public static Matrix operator- (double s1, Matrix m2)
        {
            Matrix results = new Matrix (m2.Rows, m2.Cols);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results[i, j] = s1 - m2.data[i, j];
                }
            }

            return results;
        }

        //****************************************************************************

        public static Matrix Transpose (Matrix m)
        {
            Matrix results = new Matrix (m.Cols, m.Rows);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results [i, j] = m [j, i];
                }
            }

            return results;
        }

        //****************************************************************************

        public static Matrix Inverse (Matrix m)
        {
            Matrix results = null;

            if (m.Rows != m.Cols)
                throw new Exception ("Can only invert square matrices");

            else if (m.Rows == 2)
                results = Inverse2x2 (m);

            else if (m.Rows == 3)
                results = Inverse3x3 (m);

            else if (m.Rows == 4)
                results = Inverse4x4 (m);

            else  
                results = m.GeneralInverse ();

            return results;
        }

        //****************************************************************************

        private static Matrix Inverse2x2 (Matrix m)
        {
            Matrix results = new Matrix (m.Rows, m.Cols);

            double a = m [0,0];
            double b = m [0,1];
            double c = m [1,0];
            double d = m [1,1];

            double det = a * d - b * c;

            if (Math.Abs (det) < 1e-24)
                throw new Exception (string.Format ("Determinant too small: {0}", det));

            results [0,0] =  d / det;
            results [0,1] = -b / det;
            results [1,0] = -c / det;
            results [1,1] =  a / det;

            return results;
        }

        //****************************************************************************

        private static Matrix Inverse3x3 (Matrix m)
        {
            Matrix results = new Matrix (m.Rows, m.Cols);

            double a11 = m.Get (1,1)            ;
            double a12 = m.Get (1,2);
            double a13 = m.Get (1,3);
            double a21 = m.Get (2,1);
            double a22 = m.Get (2,2);
            double a23 = m.Get (2,3);
            double a31 = m.Get (3,1);
            double a32 = m.Get (3,2);
            double a33 = m.Get (3,3);

            double det = a11 * a22 * a33 + a21 * a32 * a13 + a31 * a12 * a23 - a11 * a32 * a23 - a31 * a22 * a13 - a21 * a12 * a33;

            if (Math.Abs (det) < 1e-24)
                throw new Exception (string.Format ("Determinant too small: {0}", det));

            results.Set (1, 1, (a22 * a33 - a23 * a32) / det);
            results.Set (1, 2, (a13 * a32 - a12 * a33) / det);
            results.Set (1, 3, (a12 * a23 - a13 * a22) / det);

            results.Set (2, 1, (a23 * a31 - a21 * a33) / det);
            results.Set (2, 2, (a11 * a33 - a13 * a31) / det);
            results.Set (2, 3, (a13 * a21 - a11 * a23) / det);

            results.Set (3, 1, (a21 * a32 - a22 * a31) / det);
            results.Set (3, 2, (a12 * a31 - a11 * a32) / det);
            results.Set (3, 3, (a11 * a22 - a12 * a21) / det);

            return results;
        }

        //****************************************************************************

        private static Matrix Inverse4x4 (Matrix m)
        {
            Matrix results = new Matrix (m.Rows, m.Cols);

            double m00 = m [0,0];
            double m01 = m [0,1];
            double m02 = m [0,2];
            double m03 = m [0,3];
            double m10 = m [1,0];
            double m11 = m [1,1];
            double m12 = m [1,2];
            double m13 = m [1,3];
            double m20 = m [2,0];
            double m21 = m [2,1];
            double m22 = m [2,2];
            double m23 = m [2,3];
            double m30 = m [3,0];
            double m31 = m [3,1];
            double m32 = m [3,2];
            double m33 = m [3,3];

            double det =
               m03*m12*m21*m30 - m02*m13*m21*m30 - m03*m11*m22*m30 + m01*m13*m22*m30+
               m02*m11*m23*m30 - m01*m12*m23*m30 - m03*m12*m20*m31 + m02*m13*m20*m31+
               m03*m10*m22*m31 - m00*m13*m22*m31 - m02*m10*m23*m31 + m00*m12*m23*m31+
               m03*m11*m20*m32 - m01*m13*m20*m32 - m03*m10*m21*m32 + m00*m13*m21*m32+
               m01*m10*m23*m32 - m00*m11*m23*m32 - m02*m11*m20*m33 + m01*m12*m20*m33+
               m02*m10*m21*m33 - m00*m12*m21*m33 - m01*m10*m22*m33 + m00*m11*m22*m33;

            if (Math.Abs (det) < 1e-24)
                throw new Exception (string.Format ("Determinant too small: {0}", det));

            results [0,0] = m12*m23*m31 - m13*m22*m31 + m13*m21*m32 - m11*m23*m32 - m12*m21*m33 + m11*m22*m33;
            results [0,1] = m03*m22*m31 - m02*m23*m31 - m03*m21*m32 + m01*m23*m32 + m02*m21*m33 - m01*m22*m33;
            results [0,2] = m02*m13*m31 - m03*m12*m31 + m03*m11*m32 - m01*m13*m32 - m02*m11*m33 + m01*m12*m33;
            results [0,3] = m03*m12*m21 - m02*m13*m21 - m03*m11*m22 + m01*m13*m22 + m02*m11*m23 - m01*m12*m23;
            results [1,0] = m13*m22*m30 - m12*m23*m30 - m13*m20*m32 + m10*m23*m32 + m12*m20*m33 - m10*m22*m33;
            results [1,1] = m02*m23*m30 - m03*m22*m30 + m03*m20*m32 - m00*m23*m32 - m02*m20*m33 + m00*m22*m33;
            results [1,2] = m03*m12*m30 - m02*m13*m30 - m03*m10*m32 + m00*m13*m32 + m02*m10*m33 - m00*m12*m33;
            results [1,3] = m02*m13*m20 - m03*m12*m20 + m03*m10*m22 - m00*m13*m22 - m02*m10*m23 + m00*m12*m23;
            results [2,0] = m11*m23*m30 - m13*m21*m30 + m13*m20*m31 - m10*m23*m31 - m11*m20*m33 + m10*m21*m33;
            results [2,1] = m03*m21*m30 - m01*m23*m30 - m03*m20*m31 + m00*m23*m31 + m01*m20*m33 - m00*m21*m33;
            results [2,2] = m01*m13*m30 - m03*m11*m30 + m03*m10*m31 - m00*m13*m31 - m01*m10*m33 + m00*m11*m33;
            results [2,3] = m03*m11*m20 - m01*m13*m20 - m03*m10*m21 + m00*m13*m21 + m01*m10*m23 - m00*m11*m23;
            results [3,0] = m12*m21*m30 - m11*m22*m30 - m12*m20*m31 + m10*m22*m31 + m11*m20*m32 - m10*m21*m32;
            results [3,1] = m01*m22*m30 - m02*m21*m30 + m02*m20*m31 - m00*m22*m31 - m01*m20*m32 + m00*m21*m32;
            results [3,2] = m02*m11*m30 - m01*m12*m30 - m02*m10*m31 + m00*m12*m31 + m01*m10*m32 - m00*m11*m32;
            results [3,3] = m01*m12*m20 - m02*m11*m20 + m02*m10*m21 - m00*m12*m21 - m01*m10*m22 + m00*m11*m22;

            for (int i=0; i<4; i++)
                for (int j=0; j<4; j++)
                    results [i,j] /= det;

            return results;
        }

        //****************************************************************************
        //
        // ToString ()
        //

        public override String ToString ()
        {
            string str = null;

            for (int i = 0; i<Rows; i++)
            {
                for (int j = 0; j<Cols; j++)
                    str += data [i, j].ToString () + ",  ";

                str += "\n";
            }

            return str;
        }

        public String ToString (string format)
        {
            string str = null;

            for (int i = 0; i<Rows; i++)
            {
                for (int j = 0; j<Cols; j++)
                {
                    double d = data [i, j];

                    if (d >= 0) str += data [i, j].ToString (" " + format) + ",  ";
                    else        str += data [i, j].ToString (format) + ",  ";
                }

                str += "\n";
            }

            return str;
        }
    }
}




