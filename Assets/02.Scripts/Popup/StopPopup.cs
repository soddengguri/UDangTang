using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPopup : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        Debug.Log("Yes button clicked");

        // 이전에 진행되던 작업을 중단하고, 진행되기 전 상태로 돌리는 코드 추가
        // 예를 들어, 작업 중인 프로세스를 중단하고 초기 상태로 되돌리는 등의 로직을 추가

        // 팝업 닫기
        PopupManager.instance.CloseActivePopup();
        FindObjectOfType<Stage>().ResetProcess();
    }

    public void OnNoButtonClicked()
    {
        PopupManager.instance.CloseActivePopup();
    }
}
