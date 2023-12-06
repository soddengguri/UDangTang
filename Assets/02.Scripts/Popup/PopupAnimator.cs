using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Container.Popup
{
    public enum PopupAnimationType
    {
        None = 0,
        Alpha = 1,
        Scale = 2,
        LeftToRight = 3,
        TopToBottom = 4,
    }

    public class PopupAnimator : MonoBehaviour
    {
        protected const float MOTION_SPEED = 0.5f;

        public LeanTweenType tweenType = LeanTweenType.easeOutBack;

        private RectTransform rect_Target = null;

        private void Awake()
        {
            if (rect_Target == null)
                rect_Target = GetComponent<RectTransform>();
        }


        public virtual void startAnimation(PopupAnimationType animationType)
        {
            if (rect_Target == null)
                rect_Target = GetComponent<RectTransform>();

            switch (animationType)
            {
                case PopupAnimationType.Scale:
                    {
                        rect_Target.localScale = Vector3.zero;
                        LeanTween.scale(rect_Target, Vector3.one, MOTION_SPEED)
                            .setEase(tweenType)
                            .setIgnoreTimeScale(true);
                        break;
                    }
                case PopupAnimationType.Alpha:
                    {
                        if (!GetComponent<CanvasGroup>())
                            gameObject.AddComponent<CanvasGroup>();

                        GetComponent<CanvasGroup>().alpha = 0;
                        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, MOTION_SPEED)
                            .setEase(tweenType)
                            .setIgnoreTimeScale(true);
                        break;
                    }
                case PopupAnimationType.LeftToRight:
                    {
                        rect_Target.localPosition = new Vector3(-Screen.width, 0);
                        LeanTween.move(rect_Target, Vector2.zero, MOTION_SPEED)
                            .setEase(tweenType)
                            .setIgnoreTimeScale(true);
                        break;
                    }
                case PopupAnimationType.TopToBottom:
                    {
                        rect_Target.localPosition = new Vector3(0, Screen.height);
                        LeanTween.move(rect_Target, Vector2.zero, MOTION_SPEED)
                            .setEase(tweenType)
                            .setIgnoreTimeScale(true);
                        break;
                    }
            }
        }
    }
}