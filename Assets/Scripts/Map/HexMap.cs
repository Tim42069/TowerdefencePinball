using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public TileList tilePrefabs;
    private List<GameObject> _tiles = new List<GameObject>();
    private Dictionary<Hex, Tile> coordMap = new Dictionary<Hex, Tile>();

    public enum Tiles
    {
        Plains,
        Forrest,
        Water,
        Desert,
        Rock,
    }

    Dictionary<Hex, Tiles> map;
    Layout layout;
    int qMax = 13;
    float size;

    public static HexMap instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new HexMap();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MapDestroy();
        Initialise();
        generateMap();
        spawnTiles();

    }

    private void MapDestroy()
    {
        this.transform.rotation *= Quaternion.Euler(6.5f, 0, 0);
        //map = new Dictionary<Hex, Tiles>();
        foreach (var tile in _tiles)
        {
            
            try
            {
                Destroy(tile);
            } catch {
                
                DestroyImmediate(tile);
            }
            Destroy(tile);
        }
    }
    /*
    private void OnDisable()
    {
        MapDestroy();
    }

    private void OnApplicationQuit()
    {
        MapDestroy();
    }
*/
    private void Initialise()
    {
        if (map != null)
        {
            return;
        }
        size = 1f;
        map = new Dictionary<Hex, Tiles>();
        layout = new Layout(Orientation.flat, size,
            new Vector2
            {
                x = transform.position.x,
                y = transform.position.y
            }
            );
        for (int q = -qMax; q <= qMax; q++)
        {
            int r1 = math.max(-qMax, -q - qMax);
            int r2 = math.min(qMax, -q + qMax);
            for (int r = r1; r <= r2; r++)
            {
                Hex h = new Hex(q, r);
                map.Add(h, Tiles.Plains);
            }
        }
    }

    private void generateMap()
    {
        MapGenerator generator = new MapGenerator(qMax);
        // generator.Generate(ref map);
    }

    private void spawnTiles()
    {
        foreach (Hex key in map.Keys)
        {
            Vector2 position = layout.hexToPixel(key);
            float rotationY = layout.orientation == Orientation.pointy ? 0f : 90f;
            Quaternion finalRotation = Quaternion.Euler(0, rotationY, 0);
            GameObject tile = Instantiate(getTilePrefab(map[key]), new Vector3 { x = position.x, y = 0, z = position.y }, finalRotation, transform);
            Tile data = tile.GetComponentInChildren<Tile>();
        }
        this.transform.rotation *= Quaternion.Euler(-6.5f, 0, 0);
    }

    ref GameObject getTilePrefab(Tiles tile)
    {
        return ref tilePrefabs.Tiles[(int)tile];
    }

    public bool towerInRadius(Hex coord)
    {
        if (coordMap[coord].HasTower)
        {
            return true;
        }
        for (int i = 0; i < Hex.directions.Count; i++ )
        {
            Hex neighboor = coord.neighbor(i);
            
            if (coordMap[neighboor] != null && coordMap[neighboor].HasTower)
            {
                return true;
            }
        }
        return false;
    }
}
