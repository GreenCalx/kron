using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour
{
    [Header("Manual Refs")]
    public Animator AnimController;
    public GameCamera FPSCamera;

    public GameObject prefab_sword;
    public Transform h_handWeaponSlot;
    public Transform h_backWeaponSlot;

    [Header("Tweaks")]
    public bool freeze_inputs = false;
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float deadzone = 0.1f;
    public bool initSwordIsDrawn = false;

    [Header("Internals")]
    public Rigidbody self_rb;
    public float hMove, vMove;
    public bool playerDoAction;
    private bool isMoving = false;
    private bool pSwordIsDrawn = false;
    public bool swordIsDrawn
    {
        get {return pSwordIsDrawn;}
        set { 
            pSwordIsDrawn = value;
            ToggleSword();
            }
    }

    private GameObject h_backSword;
    private GameObject h_handSword;
    private bool isInAnimation = false;

    public string previousScene = string.Empty;

    void Start()
    {
        if (self_rb==null)
            self_rb = GetComponent<Rigidbody>();
        hMove = 0f;
        vMove = 0f;
        pSwordIsDrawn = initSwordIsDrawn;

        refreshWeapons();
    }

    private void refreshWeapons()
    {
        h_backSword = Instantiate(prefab_sword);
        h_backSword.transform.parent = h_backWeaponSlot;
        h_backSword.transform.localPosition = Vector3.zero;
        h_backSword.transform.localRotation = Quaternion.identity;


        h_handSword = Instantiate(prefab_sword);
        h_handSword.transform.parent = h_handWeaponSlot;
        h_handSword.transform.localPosition = Vector3.zero;
        h_handSword.transform.localRotation = Quaternion.identity;

        h_backSword.SetActive(!pSwordIsDrawn);
        h_handSword.SetActive(pSwordIsDrawn);
    }

    public void ToggleSword()
    {
        h_backSword.SetActive(!pSwordIsDrawn);
        h_handSword.SetActive(pSwordIsDrawn);
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

        public void DrawSword()
        {
            if (isInAnimation)
                return;
            StartCoroutine(DrawSwordAnim());
        }

        public void SheatheSword()
        {
            if (isInAnimation)
                return;
            StartCoroutine(SheatheSwordAnim());
        }

        // animations
        public void PlayThrownOutOfBalenos(Transform iThrower, float iThrowForce)
        {
            AnimController.SetTrigger("IsThrown");
            

            Vector3 throw_dir = iThrower.position - transform.position;
            self_rb.AddForce(throw_dir.normalized * -1 * iThrowForce, ForceMode.VelocityChange);
            StartCoroutine(DrawSwordAnim());
            StartCoroutine(FreezeUntilBackToIdle());
        }

        IEnumerator DrawSwordAnim()
        {
            isInAnimation = true;

            string targetAnimState = "SwordOut";
            AnimController.SetTrigger("ToggleSword");
            AnimatorStateInfo currClip = AnimController.GetCurrentAnimatorStateInfo(0);
            while (!currClip.IsName(targetAnimState))
            {
                currClip = AnimController.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }
            swordIsDrawn = true;

            isInAnimation = false;
        }

        IEnumerator SheatheSwordAnim()
        {
            isInAnimation = true;

            string targetAnimState = "SwordOut";
            AnimController.SetTrigger("ToggleSword");
            AnimatorStateInfo currClip = AnimController.GetCurrentAnimatorStateInfo(0);
            while (!currClip.IsName(targetAnimState))
            {
                currClip = AnimController.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }
            swordIsDrawn = false;

            isInAnimation = false;
        }

        IEnumerator FreezeUntilBackToIdle()
        {
            freeze_inputs = true;
            AnimatorStateInfo currClip = AnimController.GetCurrentAnimatorStateInfo(0);
            // wait to be in set animation first
            while(currClip.IsName("Idle"))
            {
                currClip = AnimController.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }
            
            // then wait for the animation to finish
            while(!currClip.IsName("Idle"))
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
