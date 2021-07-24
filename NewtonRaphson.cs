
using System;

namespace CommonMath
{
    public static partial class Calculus
    {
        public static double NewtonRaphsonError = 1e-6;
        public static int    NewtonRaphsonMaxCount = 10;

        //
        // Return X such that F (X) = Y
        //
        public static double NewtonRaphson (ddFunction F,
                                            double Y,        // target value
                                            double X0)       // initial guess
        {
            int count = 0;
            double Xi = X0;

            while (true)
            {
                Xi -= (F (Xi) - Y) / Fp (F, Xi);
                double Yi = F (Xi) - Y;

                if (Math.Abs (Yi) < NewtonRaphsonError)
                    break;

                if (++count == NewtonRaphsonMaxCount)
                    throw new Exception ("NewtonRaphsonMaxCount exceeded");
            }

            return Xi;
        }

        private static double Fp (ddFunction F, double x)
        {
            double dx = x / 1000;
            return (F (x + dx) - (F (x - dx))) / (2 * dx);
        }
    }
}
