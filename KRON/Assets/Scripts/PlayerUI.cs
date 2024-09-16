using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public Transform DialogUIHandle;
    public RawImage DialogRTImage;

    // Start is called before the first frame update
    void Start()
    {
        DialogUIHandle.gameObject.SetActive(false);
    }

    public void ShowDialog(Camera iDialogCam)
    {
        DialogUIHandle.gameObject.SetActive(true);
        DialogRTImage.texture = iDialogCam.activeTexture;
    }

    public void HideDialog()
    {
        DialogUIHandle.gameObject.SetActive(false);
    }


}
