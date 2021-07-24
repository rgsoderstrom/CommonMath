using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
//using System.Windows.Shapes;   //    do i have to add references?

namespace CommonMath
{
    public static partial class LinearAlgebra
    {
     //***********************************************************************************************

        public static bool SegmentIntersection (Point p1, // seg 1 end
                                                Point p2, // seg 1 end
                                                Point p3, // seg 2 end
                                                Point p4, // seg 2 end
                                                ref double t1, // seg 1
                                                ref double t2) // seg 2
        {
            double A = p4.X - p3.X;
            double B = p2.X - p1.X;
            double C = p4.Y - p3.Y;
            double D = p2.Y - p1.Y;

            double denom = A * A * D * D + B * B * C * C - 2 * A * B * C * D;

            if (Math.Abs (denom) < 1e-12)
                return false; // close to parallel

            double p = A * D * D - B * C * D;
            double q = C * B * B - A * B * D;
            double r = B * C * C - A * C * D;
            double s = A * A * D - A * B * C;

            p /= denom;
            q /= denom;
            r /= denom;
            s /= denom;

            double vb1 = p1.X - p3.X;
            double vb2 = p1.Y - p3.Y;

            t2 =   p * vb1 + q * vb2;
            t1 = -(r * vb1 + s * vb2);

            return true;
        }

     //***********************************************************************************************

     // inputs
     //     p0, p1 - line segment endpoints 
     //     p      - center of a disk

     // outputs
     //     t - line segment parameter at CPA assuming segment defined by (P0 + t * (P1 - P0))
     //         where t = 0..1
     //     dist - distance at point specified by t

        public static void SegmentPointCPA (Point p0,  // segment end
                                            Point p1,  // segment end
                                            Point p,   // point segment will betested against
                                            out double t,    // parameter at segment-point CPA
                                            out double dist) // distance at CPA 
        {
            double A = p.X  - p0.X;
            double B = p0.X - p1.X; // segment dx
            double C = p.Y  - p0.Y;
            double D = p0.Y - p1.Y;

          // handle segments that are not vertical 
            if (Math.Abs (B) > 1e-4) // 1e-4 is arbitrary
            {
                double K = -D / B;
                t = (K * C - A) / (B - K * D);
            } 

            else if (Math.Abs (p0.Y - p1.Y) > 1e-4) // line segment is close to vertical
            {                                       // but it has some y extent
                t = (p.Y - p0.Y) / (p1.Y - p0.Y);
            }

            else // segment is just a point, x & y extent both near 0
                t = 0.5;    
        
         // keep t from 0 to 1 (inclusive)
            if (t < 0) t = 0;
            if (t > 1) t = 1;

         // calculate distance at that t
            Vector v = p1 - p0;    
            Point  pc = p0 + t * v;

         // distance from CPA Point (pc) to passed-in Point p            
            dist = (p - pc).Length;
        }

     //***********************************************************************************************

     // Form a ray starting at testPoint in an arbitrary direction. If it intersects an odd number of the 
     // polygon's lines then the point is inside

        public static bool IsPointInsidePolygon (List<Point> polygon, Point testPoint)
        {
            if (polygon.Count < 3) // need at least 3 points for a polygon
                return false;

          // look for an angle such that the ray does not intersect a vertex
            double rayAngle = 0;
            List<double> angleToVertex = new List<double> ();

            foreach (Point pt in polygon)
            {
                double a = Math.Atan2 (pt.Y - testPoint.Y, pt.X - testPoint.X);
                if (a < 0) a += 2 * Math.PI;
                if (a >= 2 * Math.PI) a -= 2 * Math.PI;
                angleToVertex.Add (a);
            }

            angleToVertex.Sort ();

            for (int i=0; i<angleToVertex.Count; i++)
            {
                int i1 = i;
                int i2 = (i+1) % angleToVertex.Count;

                if (Math.Abs (angleToVertex [i2] - angleToVertex [i1]) > 10 * Math.PI / 180) // 10 degrees is arbitrary
                {
                    rayAngle = (angleToVertex [i2] + angleToVertex [i1]) / 2;
                    break;
                }
            }



            int intersectCount = 0;

            Point pointOnRay = new Point (testPoint.X + Math.Cos (rayAngle), testPoint.Y + Math.Sin (rayAngle));

            for (int i=0; i<polygon.Count; i++)
            {
                double t1 = 0, t2 = 0;

                // for an intersection t1 must be between 0 and 1 and t2 must be greater than 0
                bool doesIntersect = true;

                bool success = SegmentIntersection (polygon [i],                     // seg 1 end
                                                    polygon [(i+1) % polygon.Count], // seg 1 end
                                                    testPoint,  // seg 2 end
                                                    pointOnRay, // seg 2 end
                                                    ref t1,  // seg 1
                                                    ref t2); // seg 2

                if (success == false) // lines parallel
                    doesIntersect = false;

                if (t1 < 0)
                    doesIntersect = false;

                if (t1 > 1)  
                    doesIntersect = false;

                if (t2 < 0)
                    doesIntersect = false;

                if (doesIntersect == true)
                    intersectCount++;
            }

            return (((byte) intersectCount) & 1) == 1; // true if intersectCount is odd
        }

        //*****************************************************************************************************

        //
        // unit vector in direction of normal
        //
        public static Vector Normal (ddFunction xy, double x, double dx = 1e-6)
        {
            Vector n = new Vector ();
            Vector t;

            Point center = new Point (x, xy (x));
            Point left   = new Point (x - dx, xy (x - dx));
            Point right  = new Point (x + dx, xy (x + dx));

            bool centerValid = (double.IsNaN (center.Y) == false);
            bool leftValid   = (double.IsNaN (left.Y)   == false);
            bool rightValid  = (double.IsNaN (right.Y)  == false);

            if (leftValid && rightValid)
                t = right - left;

            else if (leftValid && centerValid)
                t = center - left;

            else if (rightValid && centerValid)
                t = right - center;

            else
                throw new ArithmeticException ();

            n.X = -t.Y; // rotate ccw 90 degrees
            n.Y = t.X;
            n.Normalize ();

            return n;
        }



        //*****************************************************************************************************

        //
        // PartitionCurve - returns list of points along a curve. Normals of successive segments
        //                  differ by angleThreshold
        //
        public static List<Point> PartitionCurve (ddFunction xyMapping, double minX, double maxX, int initialSlices = 1000)
        {
            List<Point> selected = new List<Point> ();
            
            List<Point> xyOversampled = new List<Point> ();
            List<Vector> normalsOversampled = new List<Vector> ();
            double dx = (maxX - minX) / (initialSlices - 1);

            for (int i = 0; i<initialSlices; i++)
            {
                try
                {
                    double X = minX + i * dx;
                    double Y = xyMapping (X);
                    xyOversampled.Add (new Point (X, Y));
                    normalsOversampled.Add (Normal (xyMapping, X));
                }

                catch (Exception) // ex)
                {
                    //EventLog.WriteLine (string.Format ("{0}", i));
                    //EventLog.WriteLine (ex.Message);
                }
            }

            double angleThreshold = 5; // degrees

            selected.Add (xyOversampled [0]);
            Vector lastSelectednormal = normalsOversampled [0];
            int lastSelected = 0;

            for (int i=0; i<xyOversampled.Count; i++)
            {
                double angleDifference = Vector.AngleBetween (normalsOversampled [lastSelected], normalsOversampled [i]);

                if (Math.Abs (angleDifference) > angleThreshold)
                {
                    lastSelectednormal = normalsOversampled [i];
                    selected.Add (xyOversampled [i]);
                    lastSelected = i;
                }
            }

            if (lastSelected != normalsOversampled.Count - 1)
                selected.Add (xyOversampled [normalsOversampled.Count - 1]);

            return selected;
        }

        //************************************************************************************************
        //
        // Gradient vector
        //

        // partials calculated using passed-in function

        public static Vector Gradient (Point pt, ZFromXYFunction func, double delta = 1e-6)
        {
            Point above = pt + new Vector (0,  delta);
            Point below = pt + new Vector (0, -delta);
            Point left  = pt + new Vector (-delta, 0);
            Point right = pt + new Vector ( delta, 0);

            double za = func (above.X, above.Y);
            double zb = func (below.X, below.Y);
            double zl = func (left.X,  left.Y);
            double zr = func (right.X, right.Y);

            double partialX = (zr - zl) / (2 * delta);
            double partialY = (za - zb) / (2 * delta);

            Vector grad = new Vector (partialX, partialY);
            return grad;
        }

        //**********************************************************************************

        // partials estimated with linear interpolation between ZValues

        public static Vector Gradient (Point pt, List<double> XValues, List<double> YValues, Matrix  ZValues)
        {
            int xIndex = XValues.FindLastIndex (delegate (double c) {return c < pt.X;});
            int yIndex = YValues.FindLastIndex (delegate (double c) {return c < pt.Y;});

            if (xIndex == -1 || yIndex == -1)
                throw new Exception ("Gradient - point not found");

            //if (xIndex == 0 || yIndex == 0)
            //throw new Exception ("Gradient - point on border");

            if (xIndex == XValues.Count - 1 || yIndex == YValues.Count - 1)
                throw new Exception ("Gradient - point on border");

            double dx = XValues [xIndex + 1] - XValues [xIndex];
            double dy = YValues [yIndex + 1] - YValues [yIndex];


            double partialX = (ZValues [yIndex, xIndex + 1] - ZValues [yIndex, xIndex]) / dx;
            double partialY = (ZValues [yIndex + 1, xIndex] - ZValues [yIndex, xIndex]) / dy;






            return new Vector (partialX, partialY);
        }
    }
}
            










