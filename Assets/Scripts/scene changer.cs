using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechanger : MonoBehaviour
{
    public string newscene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void scenechange()
    {
        WaveState._currentWave = 1;
        SceneManager.LoadScene(newscene);
    }

    public void quit()
    {
        Application.Quit();
    }
}
