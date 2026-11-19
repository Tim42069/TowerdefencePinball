using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerList", menuName = "Scriptable Objects/TowerList")]
public class TowerList : ScriptableObject
{
    
    public TowerData[] towerList;
    public TowerData[] wave1;
    public TowerData[] wave3;
    public TowerData[] wave5;
    public TowerData[] wave7;

    public TowerData gettowerdata(string whattower)
    {
        whattower = whattower.Replace("(Clone)","");
        whattower = whattower.Replace(" ", "");
        Debug.Log(whattower);
        foreach (TowerData i in towerList)
        {
            if (whattower == i.name)
            {
                return i;
            }
        }
        return null; 
    }
    
}
