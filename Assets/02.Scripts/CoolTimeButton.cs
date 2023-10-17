using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeButton : MonoBehaviour
{
    public Image imgFill;
    public Button button;
    public float coolTime = 10.0f;
    public Text textCoolTime;
    public bool isClicked = false;
    float leftTime = 10.0f;
    float speed = 2.0f;

    public void Init()
    {
        this.imgFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                if (leftTime < 0)
                {
                    leftTime = 0;
                    if (button)
                        button.enabled = true;
                    isClicked = true;
                }

                float filled = 1.0f - (leftTime / coolTime);
                if (imgFill)
                    imgFill.fillAmount = filled;
            }
    }

    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // 버튼 기능을 해지함.
    }
}
