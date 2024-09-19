using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour
{
    public Animator AnimController;
    [Header("Tweaks")]
    public bool freeze_inputs = false;
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float deadzone = 0.1f;

    [Header("Internals")]
    public Rigidbody self_rb;
    public float hMove, vMove;
    public bool playerDoAction;
    private bool isMoving = false;

    void Start()
    {
        if (self_rb==null)
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
        if (!freeze_inputs)
            ProcessInputs();
    }

    private void FetchInputs()
    {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
        playerDoAction = Input.GetKeyUp(KeyCode.T);

        isMoving = (hMove!=0f)||(vMove!=0f);
    }

    private void ProcessInputs()
    {

        if (!!self_rb && !isMoving)
        {
            self_rb.velocity = new Vector3(0f, self_rb.velocity.y, 0f);
            self_rb.angularVelocity = Vector3.zero;
            AnimController.SetBool("IsRunning", false);
            return;
        }

        // turn around
        if (hMove < 0f)
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,-1f*y_turn, 0f));
            isMoving = true;
            AnimController.SetBool("IsRunning", false);
        } 
        else if (hMove > 0f) 
        {
            float y_turn = turnSpeed * Time.fixedDeltaTime;
            transform.Rotate( new Vector3(0f,y_turn, 0f));
            isMoving = true;
            AnimController.SetBool("IsRunning", false);
        }
        
        // forward/backward
        if (vMove > 0f)
        {
            // move forward
            Vector3 translation = new Vector3(0f, 0f, speed * Time.fixedDeltaTime);
            transform.Translate(translation);
            isMoving = true;
            AnimController.SetBool("IsRunning", isMoving);
        } else if ( vMove < 0f )
        {
            // move backward
            Vector3 translation = new Vector3(0f, 0f, (speed/2f) * Time.fixedDeltaTime * -1);
            transform.Translate(translation);
            isMoving = true;
            AnimController.SetBool("IsRunning", false);
        }
    }

        // animations
        public void PlayThrownOutOfBalenos(Transform iThrower, float iThrowForce)
        {
            AnimController.SetTrigger("IsThrown");

            Vector3 throw_dir = iThrower.position - transform.position;
            self_rb.AddForce(throw_dir.normalized * -1 * iThrowForce, ForceMode.VelocityChange);
            StartCoroutine(FreezeUntilAnimationEnd("Thrown"));
        }

        IEnumerator FreezeUntilAnimationEnd(string iAnimationName)
        {
            freeze_inputs = true;
            AnimatorStateInfo currClip = AnimController.GetCurrentAnimatorStateInfo(0);
            // wait to be in set animation first
            while(!currClip.IsName(iAnimationName))
            {
                currClip = AnimController.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }
            
            // then wait for the animation to finish
            while(currClip.IsName(iAnimationName))
            {
                currClip = AnimController.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }

            yield return new WaitForFixedUpdate();

            self_rb.angularVelocity = Vector3.zero;
            self_rb.velocity = Vector3.zero;

            freeze_inputs = false;
        }
}
