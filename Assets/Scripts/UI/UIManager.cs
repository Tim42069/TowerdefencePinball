using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject continueUI;
    [SerializeField] private GameObject towerinfo;

    private bool infotext;


    private void OnEnable()
    {
        GameState.setUpgradeUI += SetUpgradeUI;
        GameState.setBuildUI += SetBuildUI;
        GameState.setMenuUI += SetMenuUI;
        GameState.setContinueUI += SetContinueUI;
    }

    private void OnDisable()
    {
        GameState.setUpgradeUI -= SetUpgradeUI;
        GameState.setBuildUI -= SetBuildUI;
        GameState.setMenuUI -= SetMenuUI;
        GameState.setContinueUI -= SetContinueUI;
    }

    private void SetUpgradeUI(bool isActive)
    {
        if (upgradeUI == null) return;
        upgradeUI.SetActive(isActive);
        Button button = upgradeUI.GetComponentInChildren<Button>();
        if (button == null)
        {
            return;
        }
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    
    public void SetBuildUI(bool isActive)
    {
        if (newtowerinfo.newwavetext==false)
        {
            buildUI.SetActive(isActive);
            Button button = buildUI.GetComponentInChildren<Button>();
            if (button == null)
            {
                return;
            }
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }
    private void SetMenuUI(bool isActive)
    {
        menuUI.SetActive(isActive);
        Button button = menuUI.GetComponentInChildren<Button>();
        if (button == null)
        {
            return;
        }
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    private void SetContinueUI(bool isActive)
    {
        continueUI.SetActive(isActive);
        Button button = continueUI.GetComponentInChildren<Button>();
        if (button == null)
        {
            return;
        }
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
