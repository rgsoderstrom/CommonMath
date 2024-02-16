using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMath
{
    public partial class CMatrix
    {
        protected Complex [,] data;

        public int Rows {get {return data.GetLength (0);}}
        public int Cols {get {return data.GetLength (1);}}
        public Complex [,] Data {get {return data;}}
                
        //****************************************************************************
        //
        // Constructors
        //

        public CMatrix (int rows, int cols)
        {
            data = new Complex [rows, cols];

            for (int i=0; i<Rows; i++)
                for (int j=0; j<Cols; j++)
                    data [i, j] = new Complex (0, 0);
        }

        public bool IsScalar       {get {return Rows == 1 && Cols == 1;}}
        public bool IsRowVector    {get {return Rows == 1 && Cols >  1;}}
        public bool IsColumnVector {get {return Rows >  1 && Cols == 1;}}

        public Complex AsScalar
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
        // operators and indexers
        //

        public Complex this [int row, int col]  // zero-based indices
        {
            get {return data [row, col];}
            set {data [row, col] = value;}
        }

        public Complex Get (int row, int col)  // one-based indices
        {
            return this [row-1, col-1];
        }

        public void Set (int row, int col, Complex value)  // one-based indices
        {
            this [row-1, col-1] = value;
        }

        //*************************************************************************************
        //
        // Addition
        //
        public static CMatrix operator+ (CMatrix m1, CMatrix m2)
        {
            //if (m1.Rows == 1 && m1.Cols == 1)
            //{
            //    return m1 [0, 0] + m2;
            //}

            //else if (m2.Rows == 1 && m2.Cols == 1)
            //{
            //    return m1 + m2 [0, 0];
            //}

            //else
            //{
                if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                    throw new Exception ("Complex matrix addition size error");

                CMatrix results = new CMatrix (m1.Rows, m1.Cols);

                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        results [i, j] = m1.data [i, j] + m2.data [i, j];
                    }
                }

                return results;
            //}
        }

        //*************************************************************************************
        //
        // Subtraction 
        //
        public static CMatrix operator- (CMatrix m1, CMatrix m2)
        {
            //if (m1.Rows == 1 && m1.Cols == 1)
            //{
            //    return m1 [0, 0] - m2;
            //}

            //else if (m2.Rows == 1 && m2.Cols == 1)
            //{
            //    return m1 - m2 [0, 0];
            //}

            //else
            //{
                if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                    throw new Exception ("Complex matrix subtraction size error");

                CMatrix results = new CMatrix (m1.Rows, m1.Cols);

                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        results [i, j] = m1.data [i, j] - m2.data [i, j];
                    }
                }

                return results;
            //}
        }

        //************************************************************************************
        //
        // Multiplication
        //
        public static CMatrix operator* (CMatrix m1, CMatrix m2)
        {
            //if (m1.Rows == 1 && m1.Cols == 1)
            //{
            //    return m1 [0, 0] * m2;
            //}

            //else if (m2.Rows == 1 && m2.Cols == 1)
            //{
            //    return m1 * m2 [0, 0];
            //}

            //else
            //{

                if (m1.Cols != m2.Rows)
                    throw new Exception ("Complex matrix multiply size error");

                CMatrix results = new CMatrix (m1.Rows, m2.Cols);

                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        for (int k = 0; k < m1.Cols; k++)
                        {
                            results [i, j] += m1.data [i, k] * m2.data [k, j];
                        }
                    }
                }

                return results;
            //}
        }

        //****************************************************************************

        public void FillByColumn (Complex [] src)
        {
            if (Rows * Cols != src.GetLength (0))
                throw new Exception ("Size error in FillByColumn");

            int get = 0;

            for (int i=0; i<Cols; i++)
                for (int j=0; j<Rows; j++)
                    data [j,i] = src [get++];
        }

        //****************************************************************************

        public static CMatrix Transpose (CMatrix m)
        {
            CMatrix results = new CMatrix (m.Cols, m.Rows);

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

        public static CMatrix Inverse (CMatrix m)
        {
            CMatrix results = null;

            if (m.Rows != m.Cols)
                throw new Exception ("Can only invert square matrices");

            else if (m.Rows == 2)
                results = Inverse2x2 (m);

            else if (m.Rows == 3)
                results = Inverse3x3 (m);

            else if (m.Rows == 4)
                results = Inverse4x4 (m);

            else  
                throw new NotImplementedException ("General complex matrix inverse not implemented");
                //results = m.GeneralInverse ();

            return results;
        }

        //****************************************************************************

        private static CMatrix Inverse2x2 (CMatrix m)
        {
            CMatrix results = new CMatrix (m.Rows, m.Cols);

            Complex a = m [0,0];
            Complex b = m [0,1];
            Complex c = m [1,0];
            Complex d = m [1,1];

            Complex det = a * d - b * c;

            if (det.Magnitude < 1e-24)
                throw new Exception (string.Format ("Determinant too small: {0}", det));

            results [0,0] =  d / det;
            results [0,1] = -b / det;
            results [1,0] = -c / det;
            results [1,1] =  a / det;

            return results;
        }

        //****************************************************************************

        private static CMatrix Inverse3x3 (CMatrix m)
        {
            CMatrix results = new CMatrix (m.Rows, m.Cols);

            Complex a11 = m.Get (1,1)            ;
            Complex a12 = m.Get (1,2);
            Complex a13 = m.Get (1,3);
            Complex a21 = m.Get (2,1);
            Complex a22 = m.Get (2,2);
            Complex a23 = m.Get (2,3);
            Complex a31 = m.Get (3,1);
            Complex a32 = m.Get (3,2);
            Complex a33 = m.Get (3,3);

            Complex det = a11 * a22 * a33 + a21 * a32 * a13 + a31 * a12 * a23 - a11 * a32 * a23 - a31 * a22 * a13 - a21 * a12 * a33;

            if (det.Magnitude < 1e-24)
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

        private static CMatrix Inverse4x4 (CMatrix m)
        {
            CMatrix results = new CMatrix (m.Rows, m.Cols);

            Complex m00 = m [0,0];
            Complex m01 = m [0,1];
            Complex m02 = m [0,2];
            Complex m03 = m [0,3];
            Complex m10 = m [1,0];
            Complex m11 = m [1,1];
            Complex m12 = m [1,2];
            Complex m13 = m [1,3];
            Complex m20 = m [2,0];
            Complex m21 = m [2,1];
            Complex m22 = m [2,2];
            Complex m23 = m [2,3];
            Complex m30 = m [3,0];
            Complex m31 = m [3,1];
            Complex m32 = m [3,2];
            Complex m33 = m [3,3];

            Complex det =
               m03*m12*m21*m30 - m02*m13*m21*m30 - m03*m11*m22*m30 + m01*m13*m22*m30+
               m02*m11*m23*m30 - m01*m12*m23*m30 - m03*m12*m20*m31 + m02*m13*m20*m31+
               m03*m10*m22*m31 - m00*m13*m22*m31 - m02*m10*m23*m31 + m00*m12*m23*m31+
               m03*m11*m20*m32 - m01*m13*m20*m32 - m03*m10*m21*m32 + m00*m13*m21*m32+
               m01*m10*m23*m32 - m00*m11*m23*m32 - m02*m11*m20*m33 + m01*m12*m20*m33+
               m02*m10*m21*m33 - m00*m12*m21*m33 - m01*m10*m22*m33 + m00*m11*m22*m33;

            if (det.Magnitude < 1e-24)
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

        //public String ToString (string format)
        //{
        //    string str = null;

        //    for (int i = 0; i<Rows; i++)
        //    {
        //        for (int j = 0; j<Cols; j++)
        //        {
        //            Complex d = data [i, j];

        //            if (d >= 0) str += data [i, j].ToString (" " + format) + ",  ";
        //            else        str += data [i, j].ToString (format) + ",  ";
        //        }

        //        str += "\n";
        //    }

        //    return str;
        //}
    }
}
