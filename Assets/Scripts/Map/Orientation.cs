using System;
using Unity.Mathematics;
using UnityEngine;

// Basic implementation following https://www.redblobgames.com/grids/hexagons/implementation.html and converted into C#
public class Orientation
{
    public readonly double f0, f1, f2, f3;
    public readonly double b0, b1, b2, b3;
    public readonly double start_angle; // In multiples of 60�

    public Orientation(double _f0, double _f1, double _f2, double _f3, double _b0, double _b1, double _b2, double _b3, double _start_angle)
    {
        f0 = _f0;
        f1 = _f1;
        f2 = _f2;
        f3 = _f3;
        b0 = _b0;
        b1 = _b1;
        b2 = _b2;
        b3 = _b3;
        start_angle = _start_angle;
    }

    public static readonly Orientation pointy = new Orientation(
        Mathf.Sqrt(3f), Mathf.Sqrt(3f)/2f, 0f, 3f/2f,
        Mathf.Sqrt(3f)/3f, -1f/3f, 0f, 2f/3f, 0f
    );

    public static readonly Orientation flat = new Orientation(
        3f/2f, 0f, Mathf.Sqrt(3f)/2f, Mathf.Sqrt(3f),
        2f/3f, 0f, -1f/3f, Mathf.Sqrt(3f)/3f, 0.5f
    );
}
