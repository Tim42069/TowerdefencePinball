using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;

public class Layout
{
    public class FractionalHex
    {
        readonly double q, r, s;
        public FractionalHex(double _q, double _r)
        {
            q = _q;
            r = _r;
            s = -q - r;
        }

        public Hex hexRound()
        {
            int _q = (int)(math.round(q));
            int _r = (int)(math.round(r));
            int _s = (int)(math.round(s));
            double q_diff = math.abs(_q - q);
            double r_diff = math.abs(_r - r);
            double s_diff = math.abs(_s - s);
            if (q_diff > r_diff && q_diff > s_diff) {
                _q = -_r - _s;
            } else if (r_diff > s_diff)
            {
                _r = -_q - _s;
            }

            return new Hex(_q, _r);
        }
    }
    public readonly Orientation orientation;
    readonly Vector2 size;
    readonly Vector2 origin;
    public Layout(Orientation _orientation, float hexSize, Vector2 _origin)
    {
        orientation = _orientation;
        origin = _origin;
        
        size = new Vector2(hexSize, hexSize);
    }

    public Vector2 hexToPixel(Hex hex)
    {
        float x = (float)((this.orientation.f0 * hex.q + this.orientation.f1 * hex.r) * size.x);
        float y = (float)(this.orientation.f2 * hex.q + this.orientation.f3 * hex.r) * size.y;

        return new Vector2 {x = x, y = y } + origin;
    }
    FractionalHex pixelToHexFractional(Vector2 point)
    {
        Vector2 pt = new Vector2
        {
            x = (point.x - origin.x) / size.x,
            y = (point.y - origin.y) / size.y,
        };

        double q = orientation.b0 * pt.x + orientation.b1 * pt.y;
        double r = orientation.b2 * pt.x + orientation.b3 * pt.y;

        return new FractionalHex(q, r);
    }

    public Hex pixelToHexRounded(Vector2 point)
    {
        return pixelToHexFractional(point).hexRound();
    }

    Vector2 hexCornerOffset(int corner)
    {
        double angle = 2.0 * math.PI *
            (orientation.start_angle + corner) / 6;
        return new Vector2
        {
            x = (float)(size.x * math.cos(angle)),
            y = (float)(size.y * math.sin(angle))
        };
    }

    List<Vector2> polygonCorners(Hex hex)
    {
        List<Vector2> corners = new List<Vector2> { };
        Vector2 centre = this.hexToPixel(hex);
        for (int i = 0; i < 6; i++)
        {
            Vector2 offset = this.hexCornerOffset(i);
            corners.Add(new Vector2
            {
                x = centre.x + offset.x,
                y = centre.y + offset.y
            });
        }
        return corners;
    }

}
