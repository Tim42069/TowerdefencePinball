using System;
using System.Collections.Generic;
using UnityEngine;

// Basic implementation following https://www.redblobgames.com/grids/hexagons/implementation.html and converted into C#
public class Hex
{
    public int q, r;
    public int s
    {
        get { return -q - r; }
    }
    
    public Hex(int _q, int _r)
    {
        q = _q;
        r = _r;
        if (_q + _r + this.s != 0)
        {
            throw new ArgumentException($"q + r should be equal to {-s}");
        }
    }

    public int length(Hex a)
    {
        return (Math.Abs(a.q) + Math.Abs(a.r) + Math.Abs(a.s)) / 2;
    }
    public int length()
    {
        return length(this);
    }

    public int distance(Hex other)
    {
        return length(this - other);
    }

    static public List<Hex> directions = new List<Hex> { 
        new Hex(1, 0), 
        new Hex(0, -1), 
        new Hex(1, -1), 
        new Hex(-1, 0), 
        new Hex(0, 1),
        new Hex(-1, 1), 
    };

    public static Hex Direction(int direction)
    {
        if (0 <= direction && direction < 6)
        {
            throw new ArgumentException("Direction should be any value of 0 - 5 inclusive");
        }
        return directions[direction];
    }

    public Hex neighbor(int direction)
    {
        return this + Direction(direction);
    }

    public static bool operator == (Hex a, Hex b)
    {
        return a.q == b.q && a.r == b.r;
    }
    public static bool operator != (Hex a, Hex b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(q, r);
    }

    public static Hex operator + (Hex a, Hex b)
    {
        return new Hex(a.q + b.q, a.r + b.r);
    }
    public static Hex operator - (Hex a, Hex b)
    {
        return new Hex(a.q - b.q, a.r - b.r);
    }
    public static Hex operator * (Hex a, int b)
    {
        return new Hex(a.q * b, a.r * b);
    }

    public override bool Equals(object obj)
    {
        if (obj is not Hex) return false;
        var item = obj as Hex;
        return q == item.q && r == item.r;

    }
}
