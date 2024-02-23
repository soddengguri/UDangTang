using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    private PopupManager popupManager;
    private StageManager stageManager;

    public int stageIndex;
    public float coolTime = 10.0f;
    public Text textCoolTime;
    public Text txtStageName;
    public Text txtStageTime;
    public Image imgFill;
    public Button button;
    public bool isClicked = false;
    public bool ingClicked = false;
    public string now;

    private float leftTime = 10.0f;
    private float speed = 2.0f;

    internal static object stageTrip;
    public static bool YesClicked { get; set; }

    private string[] stageNames = new string[]
    {
        "입구", "시작의 땅", "마법사 마을", "신전", "왕정국가", "드워프마을", "수련의 땅", "서커스단", "저승", "천체관측소"
    };

    private string[] stageTimes = new string[]
    {
        "0초", "20초", "45초", "40초", "63초", "81초", "60초", "90초", "350초", "60초"
    };

    public void Init(int index, StageManager stageManager)
    {
        this.stageManager = stageManager;
        now = stageNames[index].ToString();
        stageIndex = index;

        //Text btnTxt = Resources.Load<Text>(StageName[index]);
        txtStageName.text = stageNames[index].ToString();
        txtStageTime.text = stageTimes[index].ToString();

        transform.SetParent(stageManager.ContentContainer);

        imgFill.fillAmount = 0;

        popupManager = FindObjectOfType<PopupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            Accept();
        }
    }

    public void OnClicked()
    {
        if (ingClicked)
        {
            StopProcess();
        }
        else
        {
            StartCoroutine(ExecuteOnClicked());
        }
        
    }

    private IEnumerator ExecuteOnClicked()
    {
        // 호출 순서: StartTravelPopup() -> Wait for OnYesClicked() -> Continue with the rest

        // 1. StartTravelPopup() 호출
        popupManager.StartTravelPopup();

        
        // 2. OnYesClicked()이 끝날 때까지 대기
        yield return new WaitUntil(() => YesClicked);

        // 3. 나머지 메서드 실행
        StartCoolTime();

        stageManager.currentTrip = txtStageName.text;
        stageManager.OnStageClicked(this);

        stageManager.DisableOtherStageButtons(this);
    }


    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        ingClicked = true;
        /*
        if (button)
        {
            button.enabled = false; // 버튼 기능을 해지함.
        }
        */
       
    }

    public void Accept()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime * speed;

            if (leftTime <= 0)
            {
                leftTime = 0;

                if (button)
                {
                    button.enabled = true;
                }

                isClicked = false;
                ingClicked = false;

                stageManager.EnableAllStageButtons();
                popupManager.DoneTravelPopup();
            }

            float filled = 1.0f - (leftTime / coolTime);
            if (imgFill)
            {
                imgFill.fillAmount = filled;
            }

            // 남은 시간 표시 갱신
            int secondsRemaining = Mathf.CeilToInt(leftTime);
            if (textCoolTime)
            {
                textCoolTime.text = secondsRemaining + "초 남음";
            }
        }
    }

    private void StopProcess()
    {
        leftTime = coolTime;
        ingClicked = false;

        popupManager.StopTravelPopup();
    }

    public void ResetProcess()
    {
        // 중단되어야 하는 프로세스 초기화 로직을 여기에 추가
        leftTime = coolTime;
        isClicked = false;
        ingClicked = false;

        // UI 초기화 
        if (imgFill)
        {
            imgFill.fillAmount = 0;
        }

        int secondsRemaining = Mathf.CeilToInt(leftTime);
        if (textCoolTime)
        {
            textCoolTime.text = secondsRemaining + "초 남음";
        }

        // 버튼 상태 초기화 (필요에 따라)
        if (button)
        {
            button.enabled = true;
        }
    }
}
