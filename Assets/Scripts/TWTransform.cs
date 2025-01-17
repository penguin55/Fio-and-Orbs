﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace TWLib
{
    public static class TWTransform
    {
        public static void GetCurvePoints(ref List<Vector2> points, Vector2 current, Vector2 destination, float maxHigh)
        {
            points.Clear();
            Vector2 controlPoints = GetControlPoints(current, destination, maxHigh);
            TWMath.QuadraticBezierCurve(ref points, current, destination, controlPoints);
        }

        // Make a movement like parabola with bezier curve
        public static Vector2 MoveTowardsWithParabola(Vector2 current, ref List<Vector2> points, float speed)
        {
            if (current == points[0]) points.RemoveAt(0);
            try
            {
                return Vector2.MoveTowards(current, points[0], speed * Time.deltaTime);
            } 
            catch (IndexOutOfRangeException indexEx)
            {
                return current;
            }
            catch (NullReferenceException nullEx)
            {
                return current;
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                return current;
            }
        }

        // Get the control points from 2 position we specify. The control point is generated automaticaly accordint to 2 position (start position and end position) and max height
        private static Vector2 GetControlPoints(Vector2 current, Vector2 destination, float maxHigh)
        {
            float temporaryDistance = (destination.x - current.x) / 2f;
            return new Vector2(current.x + temporaryDistance, current.y + maxHigh);
        }
    }
}