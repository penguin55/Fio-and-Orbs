using System.Collections.Generic;
using UnityEngine;

namespace TWLib
{
    public static class TWMath
    {
        private static int numpointsCurve = 20;
        public static int NumpointsCurve { get => numpointsCurve; set => numpointsCurve = value; }

        // To get quadratic bezier curve for movement of slime.
        public static void QuadraticBezierCurve(ref List<Vector2> points, Vector2 current, Vector2 destination, Vector2 control)
        {
            float point = 0;
            points.Clear();
            for (int i = 0; i < numpointsCurve + 1; i++)
            {
                point = i / (float)numpointsCurve;
                points.Add(GetQuadraticBezierPoint(current, destination, control, point));
            }
        }

        private static Vector2 GetQuadraticBezierPoint(Vector2 startPoint, Vector2 destinationPoint, Vector2 controlPoint, float point)
        {
            //Reference https://en.wikipedia.org/wiki/B%C3%A9zier_curve 
            // Using Quadratic Bezier
            return ((1 - point) * ((1 - point) * startPoint + point * controlPoint)) +
                   (point * ((1 - point) * controlPoint + (point * destinationPoint)));
        }
    }
}
