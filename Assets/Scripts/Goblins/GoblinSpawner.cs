using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject towerbrowser;
    [SerializeField] private GameObject towerui; 

    public GameObject reloadbutton;

    public GoblinList goblinPrefabs;
    public float spawnRate = 3;
    public float spawnWidth;
    //private float _timer = 0;
    private Vector3 _pos1;
    private Vector3 _pos2;
    private int amountgoblinsent;
    private float amountgoblinsinwave;

    private float goblinslefttosend;
    public static event Action<bool> endwave;
    private bool inwave=false;

    public GameObject towerinfo;


    private float goblinsforwavetext;

    [SerializeField] TextMeshProUGUI goblinballcounter;

    void SpawnGoblins()
    {
        if (WaveState._currentWave==1)
        {
            reloadbutton.SetActive(false);
        }
        amountgoblinsent += 1;
        float changeWidth = Random.Range(-spawnWidth, spawnWidth);
        Vector3 newPosition = this.transform.position;
        newPosition.x += changeWidth;
        if (WaveState._currentWave <= 10)
        {
            Instantiate(goblinPrefabs.GetWaveGoblin(WaveState._currentWave, amountgoblinsent, amountgoblinsinwave,false), newPosition, this.transform.rotation);
        }
        else
        {
            Instantiate(goblinPrefabs.GetRandomGoblin(amountgoblinsent), newPosition, this.transform.rotation);
        }
        StartCoroutine(Timer(spawnRate));
    }

    private void OnDrawGizmos()
    {
        _pos1 = this.transform.position;
        _pos1.x -= spawnWidth;
        _pos2 = this.transform.position;
        _pos2.x += spawnWidth;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_pos1, _pos2);
    }

    private void OnEnable()
    {
        WaveState.WaveStart += onwavestart;
        DamageDealer.goblinDamage += TakeDamage;
    }

    private void OnDisable()
    {
        WaveState.WaveStart -= onwavestart;
        DamageDealer.goblinDamage -= TakeDamage;
    }

    private void onwavestart(int waveNumber)
    {
        towerui.SetActive(false);
        goblinPrefabs.TotalWaveCal(0, 0, 0, true);
        amountgoblinsent = 0;
        goblinslefttosend = waveNumber;
        if (goblinslefttosend != 1)
        {
            if (goblinslefttosend < 5)
            {
                goblinslefttosend = (waveNumber - 1) * 3;
            }

            else if (goblinslefttosend >= 4&&goblinslefttosend<=WaveState.WinRound)
            {
                goblinslefttosend= Mathf.Round((waveNumber-1)*6);
            }

            else if (goblinslefttosend>WaveState.WinRound)
            {
                if (goblinslefttosend < 20)
                {
                    goblinslefttosend = Mathf.Round(Mathf.Pow(1.25f, (waveNumber)))+50;
                }
                else
                {
                    goblinslefttosend = Mathf.Round(Mathf.Pow(1.25f, (waveNumber*1.1f)))+10;
                }
               
            }

            else
            {
                Debug.Log("creating amout goblins error");
            }
                amountgoblinsinwave = goblinslefttosend;
        }
        Debug.Log("There are "+goblinslefttosend+" Goblins this round");
        inwave = true;
        goblinsforwavetext = goblinslefttosend;
        goblinballcounter.text = "Goblins left:" + goblinsforwavetext;

        if (WaveState._currentWave >= 6)
        {
            spawnRate = spawnRate * ((50f - WaveState._currentWave) / 100);
        }
        else if (WaveState._currentWave >= 49)
        {
            spawnRate = 0.01f;
        }
            
        Debug.Log(spawnRate);
        StartCoroutine(Timer(spawnRate));
    }

    private void sendenemy()
    {
        
        if (goblinslefttosend > 0)
        {
            SpawnGoblins();


            goblinslefttosend -= 1;
        }
        else if (GameObject.FindWithTag("Goblin") == null & goblinslefttosend==0)
        {
            inwave = false;
            towerinfo.SetActive(true);
            endwave?.Invoke(true);
            
        }
        else
        {
            StartCoroutine(Timer(1f));
        }
        
        
    }

    IEnumerator Timer(float howlong)
    {
        yield return new WaitForSeconds(howlong);
        if (inwave)
        {
            sendenemy();
        }
        
    }

    public void TakeDamage(float damage)
    {
        updategoblincounter();
    }

    public void updategoblincounter()
    {
        goblinsforwavetext -= 1;
        goblinballcounter.text = "Goblins left:" + goblinsforwavetext;
    }


    private void Updatetowers()
    {
        if (goblinslefttosend ==1)
        {
            towerbrowser.SetActive(false);
        }
    }
   

}
