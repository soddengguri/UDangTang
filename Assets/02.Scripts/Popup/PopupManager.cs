using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Container.Popup
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        protected GameObject dim = null;
        [SerializeField]
        protected Popup popup = null;

        protected PopupAnimator obj_Animator = null;

        protected System.Action<PopupButtonType> OnClosedPopupListener;

        public static PopupManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            if (popup == null)
                popup = GetComponentInChildren<Popup>(true);

            obj_Animator = popup.GetComponent<PopupAnimator>();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void ShowPopup(string timer, string pieces, string bonus)
        {
            ShowPopup(string.Empty, timer, pieces, bonus);
        }
        /// <summary>
        /// Basic 한 팝업 호출시 사용.
        /// </summary>
        /// <param name="title">타이틀 내용</param>
        /// <param name="contents">콘텐츠 내용</param>
        public void ShowPopup(string title, string timer, string pieces, string bonus)
        {
            PopupInfo info = new PopupInfo.Builder()
                     .SetTitle(title)
                     .SetTimer(timer)
                     .SetPieces(pieces)
                     .SetBonus(bonus)
                     .SetButtons(PopupButtonType.Confirm)
                     .Build();
            ShowPopup(info);
        }

        /// <summary>
        /// 팝업의 다양한 정보를 가지고 호출할시 사용.
        /// </summary>
        /// <param name="info">팝업 정보</param>
        public void ShowPopup(PopupInfo info)
        {
            if (popup.IsShow)
            {
                return;
            }

            if (info.PauseScene)
                Time.timeScale = 0;

            popup.OnInitialize(info);
            OnClosedPopupListener = info.Listener;
            dim.SetActive(true);
            popup.Show();
            obj_Animator.startAnimation(info.Animation);
        }

        public void HidePopup()
        {
            Time.timeScale = 1;
            dim.SetActive(false);
            popup.Hide();
        }

        public virtual void OnClosePopup(PopupButtonType type)
        {
            if (OnClosedPopupListener != null)
            {
                OnClosedPopupListener(type);
                OnClosedPopupListener = null;
            }
            HidePopup();
        }
    }
}