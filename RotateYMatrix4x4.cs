using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath

{
    public class RotateYMatrix4x4 : Matrix4x4
    {
        public RotateYMatrix4x4 (double theta)  // degrees
        {
            base[1, 1] = 1;
            base[3, 3] = 1;

            base[0, 0] = base[2, 2] = Math.Cos (theta * Math.PI / 180);
            base[2, 0] = Math.Sin (theta * Math.PI / 180);
            base[0, 2] = -1 * base[2, 0];
        }
    }
}
