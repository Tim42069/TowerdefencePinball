using System.Collections.Generic;
using UnityEngine;


public class MapGenerator
{
    readonly int qMax;

    public MapGenerator(int _qMax)
    {
        qMax = _qMax;
    }

    public Dictionary<Vector3Int, (MapGen.Tiles, float)> Generate(List<Vector3Int> coordinates)
    {
        float[,] elevationMap = PerlinNoise2D.Generate(qMax * 2 + 1 , qMax * 2 + 1);
        float[,] moistureMap = PerlinNoise2D.Generate(qMax * 2 + 1, qMax * 2 + 1);
        Dictionary<Vector3Int, (MapGen.Tiles, float)> newMap = new Dictionary<Vector3Int, (MapGen.Tiles, float)>();
        foreach ( Vector3Int coord in coordinates)
        {

            float elevation = elevationMap[coord.x + qMax, coord.y + qMax];
            float moisture = moistureMap[coord.x + qMax, coord.y + qMax];
            newMap[coord] = (Biome(elevation, moisture), elevation);
        }
        return newMap;
    }

    private MapGen.Tiles Biome(float e, float m)
    {
        if (e < 0.25)
        {
            return MapGen.Tiles.Water;
        }
        if (e > 0.65)
        {
            return MapGen.Tiles.Rock;
        }
        if (m > 0.6)
        {
            return MapGen.Tiles.Forrest;
        }
        if ( m > 0.3)
        {
            return MapGen.Tiles.Plains;
        }
        return MapGen.Tiles.Desert;


    }
}
