using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMath
{
    public class ColumnVector : Matrix
    {
        public ColumnVector (int length) : base (length, 1)
        {
        }
        
        public ColumnVector (List<double> src) : base (src.Count, 1)
        {
            for (int i=0; i<src.Count; i++)
                data [i,0] = src [i];
        }
        
        public ColumnVector (Matrix src) : base (src.Rows, src.Cols)
        {
            if (src.Cols != 1)
                throw new Exception ("Error constructing column vector from matrix");

            for (int i=0; i<src.Rows; i++)
                data [i,0] = src [i, 0];
        }
        
        public void Fill (double[] src)
        {
            if (Rows != src.GetLength (0))
                throw new Exception ("Size error in ColumnVector.Fill");

            FillByColumn (src);
        }

        //****************************************************************************
        //
        // operators and indexers
        //

        public double this [int row]  // zero-based indices
        {
            get {return data [row, 0];}
            set {data [row, 0] = value;}
        }
    }

    public class RowVector : Matrix
    {
        public RowVector (int length) : base (1, length)
        {
        }

        public void Fill (double[] src)
        {
            if (Cols != src.GetLength (0))
                throw new Exception ("Size error in RowVector.Fill");

            FillByRow (src);
        }

        public double this [int col]  // zero-based indices
        {
            get {return data [0, col];}
            set {data [0, col] = value;}
        }

 //       public static RowVector operator* (RowVector v1, Matrix m1)
   //     {
     //       Matrix r = v1 * m1;
       //     return r as RowVector;
        //}
    }

}
