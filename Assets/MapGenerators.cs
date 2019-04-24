using System;
using UnityEngine;

public static class MapGenerators
{
    public static bool[,] Circle(
        int width,
        int height, 
        float circleRadius, 
        Vector2 circleCentre,
        bool[,] bitMap = null)
    {
        if (bitMap == null)
        {
            bitMap = new bool[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitMap[i, j] = true;
                }
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Mathf.Pow(i - circleCentre.x, 2) + Mathf.Pow(j - circleCentre.y, 2) 
                        <= Mathf.Pow(circleRadius, 2))
                {
                    bitMap[i,j] = false;
                }
            }
        }
        return bitMap;
    }
}
