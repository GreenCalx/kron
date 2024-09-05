using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Tweaks")]
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float deadzone = 0.1f;

    [Header("Internals")]
    public Rigidbody self_rb;
    public float hMove, vMove;
    private bool isMoving = false;

    void Start()
    {
        self_rb = GetComponent<Rigidbody>();
        hMove = 0f;
        vMove = 0f;
    }

    void Update()
    {
        FetchInputs();
    }

    void FixedUpdate()
    {
        ProcessInputs();
    }

    private void FetchInputs()
    {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");

        isMoving = (hMove!=0f)||(vMove!=0f);
    }

    private void ProcessInputs()
    {
        if (!!self_rb && !isMoving)
        {
            self_rb.velocity = new Vector3(0f, self_rb.velocity.y, 0f);
            self_rb.angularVelocity = Vector3.zero;
            
            return;
        }

        // turn around
        if (hMove < 0f)
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,-1f*y_turn, 0f));
            isMoving = true;
        } 
        else if (hMove > 0f) 
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,y_turn, 0f));
            isMoving = true;
        }
        
        // forward/backward
        if (vMove > 0f)
        {
            // move forward
            Vector3 translation = new Vector3(0f, 0f, speed * Time.fixedDeltaTime);
            transform.Translate(translation);
            isMoving = true;
        } else if ( vMove < 0f )
        {
            // move backward
            Vector3 translation = new Vector3(0f, 0f, speed * Time.fixedDeltaTime * -1);
            transform.Translate(translation);
            isMoving = true;
        }



    }
}
