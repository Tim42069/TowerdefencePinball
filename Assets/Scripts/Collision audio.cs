using UnityEngine;

public class Collisionaudio : MonoBehaviour
{


    public AudioSource myAudioSource;
    public AudioClip myAudioClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor") return;
        myAudioSource.PlayOneShot(myAudioClip);
    }


}
