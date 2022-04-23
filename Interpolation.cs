using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CommonMath
{
    public static class Interpolation
    {
        //
        // "count" is one greater than the total number of output points because
        // the input p0 is not copied to the output
        //
        private static List<Point3D> Linear (Point3D p0, Point3D p1, 
                                             int count) // output point count, NOT a multiplier
        {
            List<Point3D> results = new List<Point3D> ();

            if (count <= 2)
            {
                results.Add (p1);
            }

            else
            {
                Vector3D step = (p1 - p0) / (count - 1);

                for (int i=1; i<count; i++)
                    results.Add (p0 + i * step);
            }

            return results;
        }

        //******************************************************************************

        //
        // Linear interpolation between successive pairs of points in source list,
        // all approximately evenly spaced
        //

        public static List<Point3D> Linear (List<Point3D> source, 
                                            int factor) // multiplier
        {
            if (factor < 1)
                throw new Exception ("CommonMath.Interpolation.Linear - factor must be >= 1");

            List<Point3D> results = new List<Point3D> () {source [0]};

            if (source.Count == 0)
                return results;

            else if (source.Count == 1)
                return results;

            else // source.Count >= 2
            {
                //
                // determine length of each segment and total polyline length
                //
                double totalLength = 0;

                List<double> segmentLengths = new List<double> ();

                for (int i = 0; i<source.Count-1; i++)
                {
                    double length = (source [i+1] - source [i]).Length;
                    segmentLengths.Add (length);
                    totalLength += length;
                }

                int numberOutputPoints = source.Count * factor;
                double lengthStep = totalLength / (numberOutputPoints - 1);

                for (int i=0; i<source.Count-1; i++)
                {
                    double segmentLength = segmentLengths [i];
                    int    segmentSteps = (int) (0.5 + segmentLength / lengthStep);

                    List<Point3D> segmentPoints = Linear (source [i], source [i+1], segmentSteps);

                    results.AddRange (segmentPoints);
                }
            }

            return results;
        }
    }
}
