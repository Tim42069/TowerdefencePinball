using System;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PerlinNoise2D
{
    public static float[,] Generate(int x, int y)
    {
        int iterations = 3;
        float maxOffset = 1000.0f;
        float[] xOffsets = new float [10]; 
        float[] yOffsets = new float [10]; 
        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < iterations; i++)
        {
            xOffsets[i] = Random.Range(0.0f, maxOffset);
            yOffsets[i] = Random.Range(0.0f, maxOffset);
        }
        
        float[,] map = new float[y,x];
        for (float i = 0.0f; i < y; i++)
        {
            for (float j = 0.0f; j < x; j++)
            {
                float xCoord = j / x;
                float yCoord = i / y;
                // Debug.Log()
                float e = GetNoise(xCoord, yCoord, iterations, xOffsets, yOffsets);
                map[(int)i, (int)j] = Mathf.Pow((float)e, 1.2f);
                // Debug.Log(map[(int)i,(int)j]);
            }
        }
        return map;
    }


    private static float GetNoise(float x, float y, int iterations, float[] xOffsets, float[] yOffsets)
    {
        
        double e = 0;
        float amplitude = 0.5f;
        float total = 0;
        for (int i = 1; i < iterations; i++)
        {

            total += amplitude;
            e += amplitude * Mathf.PerlinNoise(Mathf.Pow(2, i) * x + xOffsets[i], Mathf.Pow(2, i) * y +yOffsets[i]);
            amplitude /= 2;
        }

        return (float)e / total;
    }

}
