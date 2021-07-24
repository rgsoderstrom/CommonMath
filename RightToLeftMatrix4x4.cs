using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath
{
    public class RightToLeftMatrix4x4 : Matrix4x4
    {
        public RightToLeftMatrix4x4 ()
        {
            base[0, 0] = -1;
            base[1, 2] =  1;
            base[2, 1] = -1;
            base[3, 3] =  1;
        }
    }
}
