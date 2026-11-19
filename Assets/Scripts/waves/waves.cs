using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Waves : MonoBehaviour
{
    public static int currentwave;
    private bool inwave;
    public static event Action<bool> wavestarted;
    public GameObject spawner;


    public GameObject Buildsystem;
    public GameObject reloadbutton;
    public GameObject winscreen;
    public GameObject winscreenbutton;
    [SerializeField] TMP_Text waveCounter;

    private int _winRound = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentwave = 1;
        inwave = false;
        waveCounter.text = string.Format("Wave :  {0} / {1}", currentwave, _winRound);
    }

    public void Startwave(InputAction.CallbackContext context)
    {
        if (!inwave)
        {
            reloadbutton.SetActive(false);
            Debug.Log(currentwave);
            inwave = true;
            this.Invoke(() => Buildsystem.SetActive(false), 0.5f);
            wavestarted?.Invoke(true);
        }
    }

    public void OnEnable()
    {
        GoblinSpawner.endwave += endwave;
    }

    private void endwave(bool endwave)
    {
        waveCounter.text = string.Format("Wave :  {0} / {1}", currentwave, _winRound);
        if (endwave)
        {
            Debug.Log("wave ended");
            if (currentwave == _winRound)
            {
                try
                {
                    _winRound = 100000;
                    winscreen.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(winscreenbutton);
                }
                catch
                {
                    Debug.Log("here2");
                }
            }
            else
            {
                try
                {
                    Buildsystem.SetActive(true);
                    currentwave += 1;
                    inwave = false;
                }
                catch
                {
                    Debug.Log("here");
                }
            }
        }
    }

    public void continueplaying()
    {
        winscreen.SetActive(false);
        endwave(true);
    }
}
