using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;

    public GameObject towerPrefab;

    [Tooltip("160 x 160 px")]
    public Texture2D icon;

    [Range(0,1000)]
    [Tooltip("In-game moneys")]
    public int cost;

    public int radius;

    public int index;
}


