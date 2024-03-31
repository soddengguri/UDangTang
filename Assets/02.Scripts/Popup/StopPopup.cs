using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPopup : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        Debug.Log("Yes button clicked");
        
        // 팝업 닫기
        PopupManager.instance.CloseActivePopup();
        FindObjectOfType<Stage>().ResetProcess();
    }

    public void OnNoButtonClicked()
    {
        PopupManager.instance.CloseActivePopup();
        FindObjectOfType<Stage>().SetIsClicked(true);
        FindObjectOfType<Stage>().SetIngClicked(true);

    }
}
