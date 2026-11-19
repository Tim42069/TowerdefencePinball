using System;
using UnityEngine;

namespace Map
{
    public struct OffsetCoord
    {
        public OffsetCoord(int col, int row)
        {
            this.col = col;
            this.row = row;
        }

        public readonly int col;
        public readonly int row;
        static public int EVEN = 1;
        static public int ODD = -1;

        static public OffsetCoord QoffsetFromCube(int offset, Hex h)
        {
            int parity = h.q & 1;
            int col = h.q;
            int row = h.r + (int)((h.q + offset * parity) / 2);
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }

            return new OffsetCoord(col, row);
        }


        static public Hex QoffsetToCube(int offset, OffsetCoord h)
        {
            int parity = h.col & 1;
            int q = h.col;
            int r = h.row - (int)((h.col + offset * parity) / 2);
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }

            return new Hex(q, r);
        }


        static public OffsetCoord RoffsetFromCube(int offset, Hex h)
        {
            int parity = h.r & 1;
            int col = h.q + (int)((h.r + offset * parity) / 2);
            int row = h.r;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }

            return new OffsetCoord(col, row);
        }


        static public Hex RoffsetToCube(int offset, OffsetCoord h)
        {
            int parity = h.row & 1;
            int q = h.col - (int)((h.row + offset * parity) / 2);
            int r = h.row;
            int s = -q - r;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }

            return new Hex(q, r);
        }

        public Vector3Int OffsetToVector()
        {
            return new Vector3Int(this.row, this.col);
        }

    }
    
}
