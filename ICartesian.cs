using System;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    public interface ICartesian
    {
        double X {get; set;}
        double Y {get; set;}
        double Z {get; set;}

        Point3D Cartesian {get; set;}
    }
}
