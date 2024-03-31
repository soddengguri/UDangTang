using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TravelState
{
    public float originalCoolTime;
    public bool originalIsClicked;
    // 다른 필요한 변수들을 추가할 수 있음
}

public class Stage : MonoBehaviour
{
    private TravelState originalState;
    private PopupManager popupManager;
    private StageManager stageManager;

    public int stageIndex;
    public float coolTime = 10.0f;
    public Text textCoolTime;
    public Text txtStageName;
    public Text txtStageTime;
    public Image imgFill;
    public Button button;
    public bool isClicked = false;  //클릭 확인
    public bool ingClicked = false; //진행 중인 지 확인
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

    // 스테이지 초기화 
    public void Init(int index, StageManager stageManager)
    {
        this.stageManager = stageManager;
        now = stageNames[index].ToString();
        stageIndex = index;

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

    // 스테이지를 클릭 시 호출
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

    // 스테이지 클릭에 대한 코루틴 실행
    private IEnumerator ExecuteOnClicked()
    {
        // 호출 순서: StartTravelPopup() -> Wait for OnYesClicked() -> Continue with the rest

        // 1. StartTravelPopup() 호출
        popupManager.StartTravelPopup(txtStageName.text);
        SaveOriginalState();

        // 2. OnYesClicked()이 끝날 때까지 대기
        yield return new WaitUntil(() => YesClicked);

        // 3. 나머지 메서드 실행
        StartCoolTime();

        stageManager.currentTrip = txtStageName.text;
        stageManager.OnStageClicked(this);

        stageManager.DisableOtherStageButtons(this);
    }

    // 여행 시작 
    public void StartCoolTime()
    {
        leftTime = coolTime;
        SetIsClicked(true);
        SetIngClicked(true);

        // 여행 시작 시 이외 팝업이 떠 있다면 닫도록 추가
        popupManager.CloseActivePopup();
    }

    // 여행 진행 메서드
    public void Accept()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime * speed;

            if (leftTime <= 0)
            {
                leftTime = 0;

                SetIsClicked(false);
                SetIngClicked(false);

                stageManager.EnableAllStageButtons();
                popupManager.DoneTravelPopup(txtStageName.text);
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

    // 여행 중지
    private void StopProcess()
    {
        // 중단 팝업 띄우기
        popupManager.StopTravelPopup(txtStageName.text);

        leftTime = coolTime;
        SetIsClicked(false);
        SetIngClicked(false);
    }

    // 여행 초기화
    public void ResetProcess()
    {
        // Json에서 상태를 읽어와 복구
        RestoreOriginalState();
        SetIsClicked(false);

        // UI 초기화 
        if (imgFill)
        {
            imgFill.fillAmount = 1;
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

    // 여행을 시작하기 전의 상태를 Json으로 저장
    public void SaveOriginalState()
    {
        originalState = new TravelState
        {
            originalCoolTime = coolTime,
            originalIsClicked = isClicked
            // 다른 필요한 변수들 저장
        };

        string json = JsonUtility.ToJson(originalState);
        PlayerPrefs.SetString("OriginalState", json);
        PlayerPrefs.Save();
    }

    // Json에서 상태를 읽어와 복구
    public void RestoreOriginalState()
    {
        string json = PlayerPrefs.GetString("OriginalState");

        if (!string.IsNullOrEmpty(json))
        {
            originalState = JsonUtility.FromJson<TravelState>(json);

            coolTime = originalState.originalCoolTime;
            isClicked = originalState.originalIsClicked;
            // 다른 필요한 변수들 복구
        }
    }

    public void SetIngClicked(bool value)
    {
        ingClicked = value;
    }

    public void SetIsClicked(bool value)
    {
        isClicked = value;
    }
}