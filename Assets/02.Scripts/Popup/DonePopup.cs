using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonePopup : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        Stage.YesClicked = false;
        PopupManager.instance.CloseActivePopup();
    }
}
