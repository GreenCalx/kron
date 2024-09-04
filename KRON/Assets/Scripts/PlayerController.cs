using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Tweaks")]
    public float speed = 10f;
    public float turnSpeed = 5f;
    void Start()
    {

    }
    void FixedUpdate()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        // turn around
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (horizontalAxis < 0f)
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,-1f*y_turn, 0f));
        } 
        else if (horizontalAxis > 0f) 
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,y_turn, 0f));
        }
        
        // forward/backward
        float verticalAxis = Input.GetAxis("Vertical");

        if (verticalAxis > 0f)
        {
            // move forward
            Vector3 translation = new Vector3(0f, 0f, speed * Time.fixedDeltaTime);
            transform.Translate(translation);
        } else if ( verticalAxis < 0f )
        {
            // move backward
            Vector3 translation = new Vector3(0f, 0f, speed * Time.fixedDeltaTime * -1);
            transform.Translate(translation);
        }


    }
}
