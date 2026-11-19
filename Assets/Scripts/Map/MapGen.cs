using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapGen : MonoBehaviour
{
    public TowerList towerList;

    [SerializeField] private Grid grid;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int mapSize;

    [Range(-30,30)]
    [SerializeField] private float rotationVal;
    public TileList tilePrefabs;
    private Dictionary<Hex,GameObject> _towerPositions = new Dictionary<Hex, GameObject>();
    
    public static MapGen instance;

    public enum Tiles
    {
        Plains,
        Forrest,
        Water,
        Desert,
        Rock,
    }
    
    public Dictionary<Vector3Int, GameObject> coordMap;

    // Make this a singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TowerBuilder.OnTowerPlacement += OnTowerPlacement;
        Generate();
    }

    private void OnDisable()
    {
        TowerBuilder.OnTowerPlacement -= OnTowerPlacement;
    }

    private void Generate()
    {
        // Destroy previous Map
        coordMap = new Dictionary<Vector3Int, GameObject>();

        // Get coordinates to spawn tiles on
        List<Vector3Int> coordinates = new List<Vector3Int>();
        
        for (int q = -mapSize; q <= mapSize; q++)
        {
            int r1 = math.max(-mapSize, -q - mapSize);
            int r2 = math.min(mapSize, -q + mapSize);
            for (int r = r1; r <= r2; r++)
            {
                Hex h = new Hex(q, r);
                OffsetCoord coord = OffsetCoord.QoffsetFromCube(-1, h);
                coordinates.Add(coord.OffsetToVector());
            }
        }

        // Spawn Randomly generated map
        Dictionary<Vector3Int, (Tiles, float)> tileMap = RandomMap(coordinates);
        Quaternion flatRotation = Quaternion.Euler(0, 90f, 0);
        foreach (var coordinate in tileMap.Keys)
        {
            var position = GetTileWorldSpace(coordinate);
            // Uncomment for elevation on tiles
            // position.y += (float)((2 * tileMap[coordinate].Item2) - 1);
            coordMap[coordinate] = Instantiate(getTilePrefab(tileMap[coordinate].Item1), position, flatRotation, transform);
            Tile tile = coordMap[coordinate].GetComponentInChildren<Tile>();
            if (tile != null)
            {
                tile.Init(coordinate);
            }
        }
        this.transform.rotation *= Quaternion.Euler(rotationVal, 0, 0);
    }

    public bool IsPositionEmpty(Vector3 origin, int radius)
    {
        Vector3Int originCoord = GetTileCoordinate(origin);
        OffsetCoord offset = new OffsetCoord(originCoord.x, originCoord.y);
        Hex originHex = OffsetCoord.RoffsetToCube(-1, offset);

        if (IsOutOfBounds(originHex)) return false;

        foreach (Hex coord in _towerPositions.Keys)
        {
            int distance = originHex.distance(coord);
            if (distance <= radius) return false;
        }
        return true;
    }

    private bool IsOutOfBounds(Hex coord)
    {
        // Area around barriers is out of bounds
        OffsetCoord offset = new OffsetCoord(0, 0);
        Hex originHex = OffsetCoord.RoffsetToCube(-1, offset);
        int distance = originHex.distance(coord);
        if (distance >= mapSize-1) return true;
        
        // Area around paddles is out of bounds
        int radius = 5;

        offset = new OffsetCoord(-mapSize, 0);
        originHex = OffsetCoord.RoffsetToCube(-1, offset);
        distance = coord.distance(originHex);
        return distance <= radius;
    }

    private void OnTowerPlacement(GameObject tower)
    {
        Hex hexCoord = GetHexFromWorldSpace(tower.transform.position);
        _towerPositions[hexCoord] = tower;
    }
    private Hex GetHexFromWorldSpace(Vector3 position)
    {
        Vector3Int coordinate = GetTileCoordinate(position);
        OffsetCoord offset = new OffsetCoord(coordinate.x, coordinate.y);
        Hex hexCoord = OffsetCoord.RoffsetToCube(-1, offset);
        return hexCoord;
    }
    public bool GetTowerOnPosition(Vector3 worldPos, out GameObject tower)
    {
        Hex hexCoord = GetHexFromWorldSpace(worldPos);
        return _towerPositions.TryGetValue(hexCoord, out tower);
    }

    public bool OnTowerRemove(GameObject tower)
    {
        Debug.Log(tower.name);
        Hex hexCoord = GetHexFromWorldSpace(tower.transform.position);
        GameEconomy.Instance.selltower(towerList.gettowerdata(tower.name));
        Debug.Log("Trying to sell tower with name: " + tower.name);
        Destroy(tower);
        return _towerPositions.Remove(hexCoord);

    }
    
    public Vector3 GetTileWorldSpace(Vector3Int coordinate)
    {
        return grid.GetCellCenterWorld(coordinate);
    }
    public Vector3Int GetTileCoordinate(Vector3 coordinate)
    {
        return grid.WorldToCell(coordinate);
    }

    Dictionary<Vector3Int, (Tiles, float)> RandomMap(List<Vector3Int> coordinates)
    {
        MapGenerator generator = new MapGenerator(mapSize);
        return generator.Generate(coordinates);
    }
    ref GameObject getTilePrefab(Tiles tile)
    {
        return ref tilePrefabs.Tiles[(int)tile];
    }
}
