using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMath
{
    public partial class Matrix
    {
        //************************************************************************************
        //
        // Multiply
        //

        public static Matrix operator* (Matrix m1, Matrix m2)
        {
            if (m1.Rows == 1 && m1.Cols == 1)
            {
                return m1 [0, 0] * m2;
            }

            else if (m2.Rows == 1 && m2.Cols == 1)
            {
                return m1 * m2 [0, 0];
            }

            else
            {

                if (m1.Cols != m2.Rows)
                    throw new Exception ("Matrix multiply size error");

                Matrix results = new Matrix (m1.Rows, m2.Cols);

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
            }
        }

        public static Matrix operator* (Matrix m1, double s2)
        {
            return s2 * m1;
        }
        
        public static Matrix operator* (double s1, Matrix m2)
        {
            Matrix results = new Matrix (m2.Rows, m2.Cols);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results[i, j] = s1 * m2.data[i, j];
                }
            }

            return results;
        }

        //************************************************************************************
        //
        // Divide
        //

        public static Matrix operator/ (Matrix m1, Matrix m2)
        {
            if (m2.Rows == 1 && m2.Cols == 1)
            {
                return m1 * (1 / m2 [0, 0]);
            }
            else
                throw new Exception ("Matrix division not supported");
        }

        public static Matrix operator/ (Matrix m1, double s)
        {
            return (1/s) * m1;
        }

        public static Matrix operator/ (double s, Matrix m1)
        {
            Matrix results = new Matrix (m1.Rows, m1.Cols);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results [i, j] = s / m1.data [i, j];
                }
            }

            return results;
        }

        //************************************************************************************
        //
        // Power
        
        // element-by-element
        public static Matrix operator^ (Matrix m1, Matrix m2)
        {
            if (m1.Rows == m2.Rows && m1.Cols == m2.Cols)
            {
                Matrix results = new Matrix (m1.Rows, m1.Cols);
            
                for (int i = 0; i < results.Rows; i++)
                {
                    for (int j = 0; j < results.Cols; j++)
                    {
                        results[i, j] = Math.Pow (m1.data[i, j], m2.data [i, j]);
                    }
                }

                return results;
            }
            else
                throw new Exception ("Matrix exponentiation argument size error");
        }

        public static Matrix operator^ (Matrix m1, double ex)
        {
            Matrix results = new Matrix (m1.Rows, m1.Cols);
            
            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results[i, j] = Math.Pow (m1.data[i, j], ex);
                }
            }

            return results;
        }

        public static Matrix operator^ (double bs, Matrix ex)
        {
            Matrix results = new Matrix (ex.Rows, ex.Cols);

            for (int i = 0; i < results.Rows; i++)
            {
                for (int j = 0; j < results.Cols; j++)
                {
                    results [i, j] = Math.Pow (bs, ex.data [i, j]);
                }
            }

            return results;
        }
    }
}
