using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int stageIndex;
    public Text TxtStageName;
    public Text TxtStageTime;

    public Image imgFill;
    public Button button;
    public float coolTime = 10.0f;

    public Text textCoolTime;
    public bool isClicked = false;

    private float leftTime = 10.0f;
    private float speed = 2.0f;

    public string now;

    StageManager stageManager;

    string[] StageName = new string[]
    {
        "입구", "시작의 땅", "마법사의 마을", "신전", "왕정국가", "드워프마을", "수련의 땅", "서커스단", "저승", "천체관측소"
    };

    string[] StageTime = new string[]
    {
        "0초", "20초", "45초", "40초", "63초", "81초", "60초", "90초", "350초", "60초"
    };

    public void Init(int index, StageManager stageManager)
    {
        this.stageManager = stageManager;
        now = StageName[index].ToString();
        stageIndex = index;

        //Text btnTxt = Resources.Load<Text>(StageName[index]);
        TxtStageName.text = StageName[index].ToString();
        TxtStageTime.text = StageTime[index].ToString();

        transform.SetParent(stageManager.ContentContainer);

        this.imgFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
         if (isClicked)
             Accept();
    }

    public void OnClicked()
    {
        StartCoolTime();
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

    private void Accept()
    {
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

}
