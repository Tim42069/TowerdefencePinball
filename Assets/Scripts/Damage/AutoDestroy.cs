using UnityEngine;
using System.Collections;
public class AutoDestroy : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
