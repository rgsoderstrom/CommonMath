using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// general matrix inverse using Gauss-Jordan elimination
// https://www.intmath.com/matrices-determinants/inverse-matrix-gauss-jordan-elimination.php
//

namespace CommonMath
{
    public partial class Matrix
    {
        //********************************************************************

        // Augmented [dst, :] += mult * Augmented [src, :]

        void RowOperation (int dst, double mult, int src)
        {
            for (int col=0; col<Cols; col++)
                data [dst, col] += mult * data [src, col];
        }

        //********************************************************************

        // divide row by value of pivot

        void MakePivotEqualOne (int N)
        {
            double divisor = data [N, N];

            for (int col = 0; col<Cols; col++)
                data [N, col] /= divisor;
        }

        //********************************************************************

        void RowSwap (int r1, int r2)
        {
            double [] wasRow1 = new double [Cols];

            for (int col = 0; col<Cols; col++)
                wasRow1 [col] = data [r1, col];

            for (int col = 0; col<Cols; col++)
                data [r1, col] = data [r2, col];

            for (int col = 0; col<Cols; col++)
                data [r2, col] = wasRow1 [col];
        }

        //********************************************************************

        Matrix MakeAugmentedMatrix ()
        {
            Matrix augmented = new Matrix (Rows, 2 * Cols);

            for (int row=0; row<Rows; row++)
            {
                for (int col = 0; col<Cols; col++)
                    augmented [row, col] = data [row, col];

                augmented [row, Cols + row] = 1;
            }

            return augmented;
        }

        //********************************************************************

        Matrix GeneralInverse ()
        {
            Matrix Augmented = MakeAugmentedMatrix ();

            for (int PivotIndex=0; PivotIndex<Cols; PivotIndex++) // number of original columns, not number of augmented columns
            {
                // if pivot point has zero, swap this row for first one below it with non-zero in that position
                if (Augmented [PivotIndex, PivotIndex] == 0)
                {
                    for (int row=PivotIndex+1; row<Rows; row++)
                    {
                        if (Augmented [row, PivotIndex] != 0)
                        {
                            Augmented.RowSwap (row, PivotIndex);
                            break;
                        }
                    }

                    // quit if didn't find one
                    if (Augmented [PivotIndex, PivotIndex] == 0)
                        throw new Exception ("GeneralInverse - matrix is singular");
                }

                Augmented.MakePivotEqualOne (PivotIndex);

                for (int row = 0; row<Rows; row++)     
                {
                    if (row != PivotIndex)
                    {
                        double mult = -1 * Augmented [row, PivotIndex];
                        Augmented.RowOperation (row, mult, PivotIndex);
                    }
                }
            }

            Matrix results = new Matrix (Rows, Cols);

            for (int row = 0; row<Rows; row++)
                for (int col = 0; col<Cols; col++)
                    results [row, col] = Augmented [row, col + Cols];

            return results;
        }
    }
}
