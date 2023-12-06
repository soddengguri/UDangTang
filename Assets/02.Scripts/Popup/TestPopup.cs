using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container.Popup
{
    public class TestPopup : MonoBehaviour
    {
        //public KeyCode key_popupConfirm;
        //public KeyCode key_popupYesNo;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                PopupInfo info = new PopupInfo.Builder()
                    .SetTitle("마법사의 마을")
                    .SetAnimation(PopupAnimationType.Alpha)
                    .SetTimer("소요시간 : 30초")
                    .SetPieces("획득 조각수 : 30개")
                    .SetBonus("보너스 발생률 : 2%")
                    .SetButtons(PopupButtonType.Start, PopupButtonType.Close)
                    .SetListener(OnClickedPopupButton)
                    .Build();

                PopupManager.Instance.ShowPopup(info);
            }
        }

        private void OnClickedPopupButton(PopupButtonType type)
        {
            Debug.Log("OnClicked_PopupButton");
            PopupManager.Instance.HidePopup();

        }
    }
}

