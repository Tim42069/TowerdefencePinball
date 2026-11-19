using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public TowerList towers;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
