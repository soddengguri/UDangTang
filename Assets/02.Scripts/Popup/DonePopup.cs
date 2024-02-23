using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonePopup : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        Debug.Log("Yes button clicked");
        Stage.YesClicked = true;
        PopupManager.instance.CloseActivePopup();
    }

    public void OnNoButtonClicked()
    {
        PopupManager.instance.CloseActivePopup();
    }
}
