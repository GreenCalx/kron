using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerUI : MonoBehaviour
{

    public Transform DialogUIHandle;
    public RawImage DialogRTImage;

    public TextMeshProUGUI dialogText;

    // Start is called before the first frame update
    void Start()
    {
        DialogUIHandle.gameObject.SetActive(false);
    }

    public void LoadDialog(string iNPCName, int iDialogID)
    {
        dialogText.text = DialogBank.GetDialog(iNPCName, iDialogID);
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
