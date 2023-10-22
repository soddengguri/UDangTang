using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int stageIndex;
    public Text TxtStageName;

    public Image imgFill;
    public Button button;
    public float coolTime = 10.0f;
    public Text textCoolTime;
    public bool isClicked = false;
    float leftTime = 10.0f;
    float speed = 2.0f;

    StageManager stageManager;

    string[] StageName = new string[]
    {
        "시작의 땅", "마법사의 마을", "신전", "왕정국가", "드워프마을", "수련의 땅", "서커스단", "저승", "천체관측소"
    };

    public void Init(int index, StageManager stageManager)
    {
        this.stageManager = stageManager;

        stageIndex = index;

        //Text btnTxt = Resources.Load<Text>(StageName[index]);
        TxtStageName.text = StageName[index].ToString();

        transform.SetParent(stageManager.ContentContainer);

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

    public void OnClicked()
    {
        // StageManager 스크립트에 있는 OnStageClicked 함수를 호출합니다.
        stageManager.OnStageClicked(this);
    }

    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // 버튼 기능을 해지함.
    }
}
