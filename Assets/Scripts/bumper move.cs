using UnityEngine;
using UnityEngine.InputSystem;

public class bumpermove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //bumpers
    public GameObject paddle;
    public GameObject anchorpoint;
    
    private bool paddlemove=false;
    public bool isleft= true;
    private float rotationvalue;
    private bool rotated=false;
    
    void Start()
    {
        if (isleft)
        {
          rotationvalue=-60f;
        }
        else
        {
          rotationvalue=60f;
        }
    }
    public void paddleget(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        paddlemove = !paddlemove;
        if (paddlemove)
        {
            paddle.transform.RotateAround(anchorpoint.transform.position, new Vector3(0, -0.5f, 0.5f), rotationvalue);
            rotated = true;

        }
        else
        {
            if (rotated)
            {
                paddle.transform.RotateAround(anchorpoint.transform.position, new Vector3(0, -0.5f, 0.5f), -rotationvalue);         
                rotated = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
