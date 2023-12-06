using UnityEngine;
using UnityEngine.UI;
using System;

namespace Container.Popup
{
    [Serializable]
    public class PopupInfoDictionary : SerializableDictionary<PopupButtonType, PopupButtonInfo> { }
    [RequireComponent(typeof(PopupAnimator))]
    public class Popup : MonoBehaviour
    {
        [SerializeField]
        protected Text txt_title = null;
        [SerializeField]
        protected Text txt_timer = null;
        [SerializeField]
        protected Text txt_pieces = null;
        [SerializeField]
        protected Text txt_bonus = null;
        [SerializeField]
        protected Transform root_button = null;
        [SerializeField]
        protected GameObject basic_PopupButton = null;

        [SerializeField]
        private PopupInfoDictionary dic_popupButtonInfo;

        public bool IsShow => this.gameObject.activeSelf;

        /// <summary>
        /// 초기화 함수.
        /// </summary>
        /// <param name="info">팝업 정보</param>
        public void OnInitialize(PopupInfo info)
        {
            txt_title.text = info.Title;
            txt_timer.text = info.Timer;
            txt_pieces.text = info.Pieces;
            txt_bonus.text = info.Bonus;
            SetButtons(info.Buttons);
        }

        protected virtual void SetButtons(PopupButtonType[] btns)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                GameObject popupObject = Instantiate(basic_PopupButton, root_button);
                popupObject.transform.localScale = Vector3.one;
                popupObject.SetActive(true);

                PopupButton popupButton = popupObject.GetComponent<PopupButton>();

                popupButton.Type = btns[i];
                try
                {
                    popupButton.Name = dic_popupButtonInfo[popupButton.Type].str_name;
                    popupButton.Color = dic_popupButtonInfo[popupButton.Type].color_button;
                }
                catch { Debug.Log("요청 타입이 PopupInfo에 정의되어 있지 않습니다."); }
            }
        }

        protected virtual void SetButtonColor()
        {
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            PopupButton[] popupButtons = root_button.GetComponentsInChildren<PopupButton>(true);

            foreach (PopupButton btn in popupButtons)
            {
                DestroyImmediate(btn.gameObject);
            }

            gameObject.SetActive(false);
        }

    }
}