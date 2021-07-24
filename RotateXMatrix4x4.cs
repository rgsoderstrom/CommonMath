using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath

{
    public class RotateXMatrix4x4 : Matrix4x4
    {
        public RotateXMatrix4x4 (double theta)  // degrees
        {
            base[0, 0] = 1;
            base[3, 3] = 1;

            base[1, 1] = base[2, 2] = Math.Cos (theta * Math.PI / 180);
            base[1, 2] = Math.Sin (theta * Math.PI / 180);
            base[2, 1] = -1 * base[1, 2];

        }
    }
}
