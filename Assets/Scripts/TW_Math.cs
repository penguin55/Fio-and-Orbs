using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TomWill
{
    public static class TW_Math
    {
        public static float GetTrigonoY(float r, float x)
        {
            float temp = Mathf.Pow(r,2) - Mathf.Pow(x,2);
            return Mathf.Sqrt(temp);
        }
    }
}
