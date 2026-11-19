using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GoblinList", menuName = "Scriptable Objects/GoblinList")]
public class GoblinList : ScriptableObject
{
    public GameObject[] goblinPrefabs;
    private float currentwave;
    float currentgoblin;

    public GameObject GetRandomGoblin(int amountgoblinsent)
    {
        int num = Random.Range(0, goblinPrefabs.Length);
        return goblinPrefabs[num];
    }

    public GameObject GetSpecificGoblin(int whatgoblin)
    {
        return goblinPrefabs[whatgoblin];
    }

    public GameObject GetWaveGoblin(int wave, int amountgoblin, float amountofgoblinsinwave,bool reset)
    {
        int whatgoblin = TotalWaveCal(wave, amountgoblin, amountofgoblinsinwave,reset);
        return goblinPrefabs[whatgoblin];
    }


    public int TotalWaveCal(int currentwave, int currentgoblininwave, float totalwaves,bool reset)
    {
        
        float currentprogress = currentgoblininwave / totalwaves;
        if (reset)
        {
            currentgoblin=0;
            return 0;
        }
        

        if (currentwave <= 2)
        {
            return 0;
        }

        else if (currentwave == 3)
        {
            if (currentprogress > (8f / 10f))
            {
                return 5;
            }
            else
            {
                return 0;
            }

           
        }

        else if (currentwave <= 5)
        {

            if (currentprogress > (6f / 10f))
            {
                currentgoblin +=1f;
                float amountspecialgoblinsleft =Mathf.Round((totalwaves * 0.4f)/2f);

                if (currentgoblin <= amountspecialgoblinsleft)
                {
                    return 5;
                }
                else 
                { 
                    return 3; 
                }
            }
            else
            {
                return 0;
            }
        }

        else if (currentwave == 6)
        {
            if (currentprogress > (5f / 10f))
            {
                currentgoblin +=1;
                float amountspecialgoblinsleft = Mathf.Round(totalwaves * 0.5f) / 3f;

                if (currentgoblin < amountspecialgoblinsleft) return 5;

                else if (currentgoblin >= amountspecialgoblinsleft && currentgoblin <= amountspecialgoblinsleft * 2) return 3;

                else { return 1; }
            }
            else { return 0; }
        }

        else if (currentwave <= 8)
        {
            if (currentprogress > (4f / 10f))
            {
                currentgoblin +=1;
                float amountspecialgoblinsleft = Mathf.Round(totalwaves * 0.6f) / 4f;

                if (currentgoblin <= amountspecialgoblinsleft) return 5;

                else if (currentgoblin >= amountspecialgoblinsleft && currentgoblin <= (amountspecialgoblinsleft * 2)) return 3;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 3)) return 1;

                else return 4;
            }
            else return 0;
        }

        else if (currentwave == 9)
        {
            if (currentprogress > (3f / 10f))
            {
                currentgoblin += 1;
                float amountspecialgoblinsleft = Mathf.Round(totalwaves * 0.7f) / 5f;

                if (currentgoblin <= amountspecialgoblinsleft) return 5;

                else if (currentgoblin >= amountspecialgoblinsleft && currentgoblin <= (amountspecialgoblinsleft * 2f)) return 3;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 3f)) return 1;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 4f)) return 4;

                else return 6;
            }
            else return 0;
        }

        else if (currentwave == 10)
        {
            if (currentprogress > (1f / 10f))
            {
                currentgoblin += 1;
                float amountspecialgoblinsleft = Mathf.Round(totalwaves * 0.9f) / 6f;

                if (currentgoblin <= amountspecialgoblinsleft) return 5;

                else if (currentgoblin >= amountspecialgoblinsleft && currentgoblin <= (amountspecialgoblinsleft * 2f)) return 3;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 3f)) return 1;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 4f)) return 4;

                else if (currentgoblin >= amountspecialgoblinsleft * 2f && currentgoblin <= (amountspecialgoblinsleft * 5f)) return 6;

                else return 2;
            }
            else return 0;
        }

        else return 0;
    }
}
