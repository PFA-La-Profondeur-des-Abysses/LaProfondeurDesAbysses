using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidHelper
{

    const int numViewDirections = 300;
    public static readonly Vector3[] directions;

    static BoidHelper()
    {
        const int numViewDirections = 300;

        Vector3[] directions = new Vector3[numViewDirections];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++)
        {
            float t = (float)i / numViewDirections;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination); //anciennemnt azimuth
            float y = Mathf.Cos(inclination); //anciennemnt azimuth

            directions[i] = new Vector3(x, y,0);

        }

    }
}