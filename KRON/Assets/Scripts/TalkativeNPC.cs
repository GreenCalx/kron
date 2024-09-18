using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TalkativeNPC : MonoBehaviour
{
    [Header("Self Refs")]
    public Camera dialogCam;
    public string dialog = "Hello world"; // TODO : make dialogbank & load lines

    [Header("Animation")]
    public Image uiImageTarget;
    public List<Sprite> spritesToAnimate;
    public float animFrameDuration = 0.5f;
    public bool animateBubble = true;
    public bool playerInTalkTrigger = false;

    private Coroutine animateBubbleCo;

    [Header("Talkative NPC Tweak")]
    public string name;
    public List<int> dialog_ids;

    [Header("Internals")]
    private bool playerIsTalking = false;
    private int currDialogIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        uiImageTarget.gameObject.SetActive(false);
        playerInTalkTrigger = false;
        animateBubble = false;
        playerIsTalking = false;
    }

    void Update()
    {
        if (animateBubble && (animateBubbleCo==null))
        {
            StartBubbleAnim();
        }
        if (!animateBubble && (animateBubbleCo!=null))
        {
            StopBubbleAnim();
        }

        if (playerInTalkTrigger)
        {
            if (Access.Player().playerDoAction)
            {
                if (!playerIsTalking)
                    StartTalk(); 
                else if (currDialogIndex < dialog_ids.Count - 1)
                    TalkNext();
                else
                    StopTalk();
            }
        }
    }

    private void StartTalk()
    {
        playerIsTalking = true;
        
        if (dialog_ids.Count>0)
        {
            currDialogIndex = 0;
            Access.PUX().LoadDialog(name, dialog_ids[currDialogIndex]);
        }
        Access.PUX().ShowDialog(dialogCam);
    }

    public void TalkNext()
    {
        currDialogIndex++;
        Access.PUX().LoadDialog(name, dialog_ids[currDialogIndex]);
    }

    private void StopTalk()
    {
        playerIsTalking = false;
        Access.PUX().HideDialog();
    }

    private void StartBubbleAnim()
    {
        uiImageTarget.sprite = spritesToAnimate[0];
        animateBubbleCo = StartCoroutine(BubbleAnimCo());

        uiImageTarget.gameObject.SetActive(true);
    }

    private void StopBubbleAnim()
    {
        StopCoroutine(animateBubbleCo);
        animateBubbleCo = null;

        uiImageTarget.gameObject.SetActive(false);
    }

    IEnumerator BubbleAnimCo()
    {
        int spriteIndex = 0;
        while (animateBubble)
        {
            uiImageTarget.sprite = spritesToAnimate[spriteIndex];
            spriteIndex++;
            if (spriteIndex >= spritesToAnimate.Count)
                spriteIndex = 0;
            yield return new WaitForSeconds(animFrameDuration);
        }
    }

    public void PlayerCanTalk(bool iState)
    {
        animateBubble = iState;
        playerInTalkTrigger = iState;

        if (!iState && playerIsTalking)
        {
            StopTalk();
        }
    }
}
