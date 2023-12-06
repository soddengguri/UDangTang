using UnityEngine;
using UnityEngine.UI;

namespace Container.Popup
{
    public class PopupButton : Button
    {
        protected PopupManager obj_manager = null;

        public PopupButtonType enm_type = PopupButtonType.None;

        public PopupButtonType Type
        {
            get { return enm_type; }
            set { enm_type = value; }
        }

        public string Name
        {
            get { return GetComponentInChildren<Text>(true).text; }
            set { GetComponentInChildren<Text>(true).text = value; }
        }

        public Color Color
        {
            get
            {
                return GetComponent<Image>().color;
            }
            set
            {
                GetComponent<Image>().color = value;
            }
        }

        protected override void Start()
        {
            base.Start();
            if (obj_manager == null)
            {
                obj_manager = this.GetComponentInParent<PopupManager>();
            }
        }


        /// <summary>
        /// 버튼 클릭시 매니저로 이벤트를 보내는 함수
        /// </summary>
        /// <param name="eventData">Event data.</param>
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (obj_manager != null)
            {
                obj_manager.OnClosePopup(enm_type);
            }
        }
    }
}