using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class bringupquit : MonoBehaviour
{
    public GameObject quitbutton;
    private bool isshowing=false;
    public void Start()
    {
        quitbutton.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void quit(InputAction.CallbackContext context)
    {
        isshowing = !isshowing;
        quitbutton.SetActive(isshowing);
        EventSystem.current.SetSelectedGameObject(quitbutton);
    }
}
