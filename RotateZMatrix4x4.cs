using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath

{
    public class RotateZMatrix4x4 : Matrix4x4
    {
        public RotateZMatrix4x4 (double theta)  // degrees
        {
            base[2, 2] = 1;
            base[3, 3] = 1;

            base[0, 0] = base[1, 1] = Math.Cos (theta * Math.PI / 180);
            base[0, 1] = Math.Sin (theta * Math.PI / 180);
            base[1, 0] = -1 * base[0, 1];
        }
    }
}
