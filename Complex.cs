using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace CommonMath
{
    public class Complex
    {
        double real, imag;

        public Complex (double re, double im)
        {
            real= re;
            imag = im;
        }

        public Complex (string str)
        {
            string [] tokens = str.Split (new char [] {' ', '+', 'i'}, StringSplitOptions.RemoveEmptyEntries);
            Real = double.Parse (tokens [0]);
            Imag = double.Parse (tokens [1]);
        }

        public double Real {get {return real;}
                            set {real = value;}}

        public double Imag {get {return imag;}
                            set {imag = value;}}

        public double  Magnitude {get {return Math.Sqrt  (Real * Real + Imag * Imag);}}
        public double  Angle     {get {return Math.Atan2 (Imag, Real);}}  // in radians
        public Complex Conjugate {get {return new Complex (Real, -1 * Imag);}}

        public static Complex operator+ (Complex op1, Complex op2) {return new Complex (op1.Real + op2.Real, op1.Imag + op2.Imag);}
        public static Complex operator- (Complex op1, Complex op2) {return new Complex (op1.Real - op2.Real, op1.Imag - op2.Imag);}

        public static Complex operator* (Complex op1, Complex op2) {return new Complex (op1.Real * op2.Real - op1.Imag * op2.Imag, op1.Real * op2.Imag + op1.Imag * op2.Real);}

        public static Complex operator/ (Complex op1, Complex op2) 
        {
            double mag = op1.Magnitude / op2.Magnitude; double ang = op1.Angle - op2.Angle; 
            return new Complex (mag * Math.Cos (ang), mag * Math.Sin (ang));
        }

        public static Complex operator+ (Complex op1, double  op2) {return new Complex (op1.Real + op2, op1.Imag);}
        public static Complex operator+ (double  op1, Complex op2) {return new Complex (op2.Real + op1, op2.Imag);}

        public static Complex operator* (Complex op1, double  op2) {return new Complex (op1.Real * op2, op1.Imag * op2);}
        public static Complex operator* (double  op1, Complex op2) {return new Complex (op2.Real * op1, op2.Imag * op1);}

        public static Complex operator- (Complex op) {return new Complex (-1 * op.Real, -1 * op.Imag);}

       public override string ToString ()
        {
            return string.Format ("({0:0.####}, {1:0.####})", Real, Imag);
        }
    }
}
