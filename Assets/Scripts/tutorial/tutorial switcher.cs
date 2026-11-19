using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
public class tutorialswitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject[] inforscreens;

    private GameObject currentscreen;
    [SerializeField] TextMeshProUGUI buttontext;
    private int counter=0;
    private bool gamebuttonactive;
    void Start()
    {
        for (int i = 0; i < inforscreens.Length; i++)
        {
            inforscreens[i].SetActive(false);
        }
            currentscreen = inforscreens[0];
        currentscreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button()
    {
        if (gamebuttonactive)
        {
            SceneManager.LoadScene("Main Scene");
        }
        counter += 1;
        currentscreen.SetActive(false);
        currentscreen = inforscreens[counter];
        currentscreen.SetActive(true) ;
        if (counter >= inforscreens.Length-1)
        {
            buttontext.text = "To Game?";
            gamebuttonactive = true;
        }

    }
}
