using UnityEngine;
using UnityEngine.InputSystem;

public class testsendingballs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GoblinList goblinPrefabs;
    private Vector3 thisposition;

    // Update is called once per frame
    void Update()
    {
        thisposition = this.transform.position;
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(0), thisposition, this.transform.rotation);
            Debug.Log("1");
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(1), thisposition, this.transform.rotation);
            Debug.Log("2");
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(2), thisposition, this.transform.rotation);
            Debug.Log("3");
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(3), thisposition, this.transform.rotation);
            Debug.Log("4");
        }
        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(4), thisposition, this.transform.rotation);
            Debug.Log("5");
        }
        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(5), thisposition, this.transform.rotation);
            Debug.Log("6");
        }
        if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            Instantiate(goblinPrefabs.GetSpecificGoblin(6), thisposition, this.transform.rotation);
            Debug.Log("7");
        }
    }
}
