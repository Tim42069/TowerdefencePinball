using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshFilter filter;
    public Vector3Int coordinate;
    public bool HasTower = false;

    public void Init(Vector3Int coord)
    {
        this.coordinate = coord;
    }
}
