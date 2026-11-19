using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TowerBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;
    private GameObject startingButton; // First button selected when build menu is selected

    private TowerData[] towerList;

    private Transform buttonposition;

    public static event Action<TowerData> OnTowerSelected;
    private bool moretowers=true;
    private void Awake()
    {
        buttonposition=buttonParent.transform;
        
    }

    private void OnEnable()
    {
        WaveState.updatelist += updatelist;
    }

    public void updatelist(int overflow)
    {
        Debug.Log(BuildManager.Instance.ToString());;
        int whatwave=WaveState._currentWave;
        if (whatwave == 1)
        {
            towerList = BuildManager.Instance.towers.wave1;
        }
        else if (whatwave == 3)
        {
            towerList = BuildManager.Instance.towers.wave3;
        }
        else if (whatwave == 5)
        {
            towerList = BuildManager.Instance.towers.wave5;
        }
        else if (whatwave == 7)
        {
            towerList = BuildManager.Instance.towers.wave7;
        }
        else
        {
            towerList = new TowerData[0];
        }
        if (overflow == whatwave&moretowers)
        {
            for (int i = 0; i < towerList.Length; i++)
            {
                GameObject newButton = Instantiate(buttonPrefab, buttonposition);

                // Give the tower select button an image of the tower, its name, and its positon
                TowerSelectButton tsb = newButton.GetComponent<TowerSelectButton>();
                tsb.towerImage.texture = towerList[i].icon;
                tsb.towerName.text = towerList[i].towerName;
                tsb.towerID = towerList[i].index;
                Button button = newButton.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    OnTowerSelected?.Invoke(BuildManager.Instance.towers.towerList[tsb.towerID]);
                });
                if (i == 0)
                {
                    EventSystem.current.SetSelectedGameObject(newButton);
                    startingButton = newButton;
                }
            }
            towerList = new TowerData[0];
            moretowers = false;
        }
        else
        {
            
        }
    }
    private void OnDisable()
    {
        moretowers=true;
    }
}
