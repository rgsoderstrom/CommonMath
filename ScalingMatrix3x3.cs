using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath

{
    public class ScalingMatrix3x3 : Matrix4x4
    {
        public ScalingMatrix3x3 (double sx, double sy, double sz)
        {
            base[0, 0] = sx;
            base[1, 1] = sy;
            base[2, 2] = sz;
            base[3, 3] = 1;
        }

        public ScalingMatrix3x3 (double s)
        {
            base[0, 0] = s;
            base[1, 1] = s;
            base[2, 2] = s;
            base[3, 3] = 1;
        }
    }
}
