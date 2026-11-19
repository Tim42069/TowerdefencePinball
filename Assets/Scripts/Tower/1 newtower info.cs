using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class newtowerinfo: MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int whatwave;
    string whattosay;
    public GameObject buildmenu;
    public GameObject gamemenu;
    public GameObject updateui;

    public static bool newwavetext;
    
    public void OnEnable()
    {

        whatwave = WaveState._currentWave;
        whattosay="";
        if (whatwave == 0)
        {
            whattosay = "Your starting towers\n\nBounce tower\nA contact type tower that deals damage and bounces goblins off when they collide with it.\n\nMagic tower\nLaunches slow moving homing projectiles that won't disappear until they hit a goblin.";
        }
        else if (whatwave == 2)
        {
            whattosay = "You Just Unlocked a new tower\n\nBallista tower\nFires fast travelling ballista bolts that fly in a straight path.";
        }
        else if (whatwave == 4)
        {
            whattosay = "You Just Unlocked a new tower\n\nObylisk tower\nFires an instant chain bolt of lightning that damages a goblin and whichever goblin is closest.\n\nJester tower\nFunctions as a support tower by slowly drawing goblins into it\nAfter Being hit by a Goblin deactivates before entering a cooldown";
        }
        else if (whatwave == 6)
        {
            whattosay = "You Just Unlocked a new tower\n\nCage tower\nCatches goblins that make contact with it, holding them captive before releasing them after a 3 second pause as a friendly goblin that damages other goblins but doesn't damage you. (Can only hold one goblin at a time)";
        }
        if (whattosay!="")
        {
            buildmenu.SetActive(false);
            gamemenu.SetActive(false);
            text.text = whattosay + "\n\n\n Press B to continue";
            newwavetext=true;
        }
        else
        {
            newwavetext=false;
            close();
        }
    }
    public void turnoff(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        if (newwavetext==true)
        {
            newwavetext=false;
            close();
        }
        }
    }

    private void close()
    {
        UIManager uiscript =updateui.GetComponent<UIManager>();
        if (this.gameObject.activeSelf)
                {
                    uiscript.SetBuildUI(true);
                    gamemenu.SetActive(true);
                    this.gameObject.SetActive(false);


                    Button button = buildmenu.GetComponentInChildren<Button>();
                    if (button == null)
                    {
                        Debug.Log("here");
                        return;
                    }
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                }
    }
}
