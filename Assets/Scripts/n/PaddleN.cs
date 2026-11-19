using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class Flipper : MonoBehaviour
{

    public string inputName;

    public float restingPosition;
    public float pressedPosition = 45f;
    public float hitStrength = 1000f;
    public float flipperDamper = 150f;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private HingeJoint hinge;

    private float move;

    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    public void bumpermove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<float>();
        if (move == 1) source.PlayOneShot(clip);

    }

    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;



        if (move == 1)
        {
            spring.targetPosition = pressedPosition;
        }
        else
        {

            spring.targetPosition = restingPosition;
        }

        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
