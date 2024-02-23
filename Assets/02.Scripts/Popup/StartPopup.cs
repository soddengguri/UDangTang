using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPopup : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        Debug.Log("Yes button clicked");
        PopupManager.instance.CloseActivePopup();
        Stage.YesClicked = true;
    }

    public void OnNoButtonClicked()
    {
        PopupManager.instance.CloseActivePopup();
    }
}
