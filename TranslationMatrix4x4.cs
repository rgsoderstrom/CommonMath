using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Common;

namespace CommonMath

{
    public class TranslationMatrix4x4 : Matrix4x4
    {
        public TranslationMatrix4x4 (double dx, double dy, double dz)
        {
            base[0, 0] = 1;
            base[1, 1] = 1;
            base[2, 2] = 1;
            base[3, 3] = 1;
            base[0, 3] = dx;
            base[1, 3] = dy;
            base[2, 3] = dz;
        }
    }
}
