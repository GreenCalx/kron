using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TalkativeNPC : MonoBehaviour
{
    [Header("Animation")]
    public Image uiImageTarget;
    public List<Sprite> spritesToAnimate;
    public float animFrameDuration = 0.5f;
    public bool animateBubble = true;
    public bool playerInTalkTrigger = false;

    private Coroutine animateBubbleCo;


    // Start is called before the first frame update
    void Start()
    {
        uiImageTarget.gameObject.SetActive(false);
        playerInTalkTrigger = false;
        animateBubble = false;
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
    }
}
