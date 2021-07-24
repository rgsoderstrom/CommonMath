using System;
using System.Collections.Generic;
using System.Text;

//using Common;

namespace CommonMath
{
    [Serializable]
    public class Matrix4x4
    {
        const int N = 4;

        protected double[,] data = new double [N, N];
        
        //****************************************************************************
        //
        // Constructors
        //

        public Matrix4x4 ()
        {
        }

        public Matrix4x4 (Matrix4x4 src)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    data[i, j] = src.data [i, j];
                }
            }
        }

        public void Unit ()
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    data[i, j] = 0;

            data [0, 0] =
            data [1, 1] =
            data [2, 2] =
            data [3, 3] = 1;
        }

        //****************************************************************************
        //
        // operators and indexers
        //

        public double this [int row, int col]  // zero based indices
        {
            get {return data [row, col];}
            set {data [row, col] = value;}
        }

        public static Matrix4x4 operator * (Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 results = new Matrix4x4 ();

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

        // assume p1 is a column vector
        public static Point4DH operator * (Matrix4x4 m1, Point4DH p1)
        {
            Point4DH results = new Point4DH ();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    results[i] += m1.data[i, j] * p1 [j];                
                }
            }

            return results;
        }

        // assume v1 is a column vector
        public static Vector4D operator * (Matrix4x4 m1, Vector4D v1)
        {
            Vector4D results = new Vector4D ();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    results[i] += m1.data[i, j] * v1 [j];                
                }
            }

            return results;
        }


        //****************************************************************************
        //
        // ToString ()
        //

        public override String ToString ()
        {
            return string.Format ("[{0}, {1}, {2}, {3}]\n", data[0, 0], data[0, 1], data[0, 2], data[0, 3])
                 + string.Format ("[{0}, {1}, {2}, {3}]\n", data[1, 0], data[1, 1], data[1, 2], data[1, 3])
                 + string.Format ("[{0}, {1}, {2}, {3}]\n", data[2, 0], data[2, 1], data[2, 2], data[2, 3])
                 + string.Format ("[{0}, {1}, {2}, {3}]\n", data[3, 0], data[3, 1], data[3, 2], data[3, 3]);
        }

    }
}
