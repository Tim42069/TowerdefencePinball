using UnityEngine;

public class BackWallGobs : MonoBehaviour
{
    private Animation[] anim;
    [SerializeField] float remainTime;
    [SerializeField] bool stop;
    public GameStateMachine gsm;

    private void Awake()
    {
        anim = GetComponentsInChildren<Animation>();

        // turn loop on for all
        foreach (Animation a in anim)
        {
            a.wrapMode = WrapMode.Loop;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayAll();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StopAll();
        }



        if (gsm.currentState == gsm.waveState)
        {
            PlayAll();
        }
        else
        {
            StopAll();
        }

        foreach (Animation a in anim)
            {
                AnimationState state = a[a.clip.name];
                // how much time remains in current loop
                remainTime = remainTime = state.length - (state.time % state.length);
            }

        if (stop)
        {
            if (remainTime <= 0.1f)
            {
                foreach (Animation a in anim)
                {
                    a.Stop();
                }
                stop = false;
            }
        }

    }

    public void PlayAll()
    {
        foreach (Animation a in anim)
        {
            // start looping
            a.wrapMode = WrapMode.Loop;
            a.Play();
        }
    }

    public void StopAll()
    {
        stop = true;
    }
}