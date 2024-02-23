using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    public Stage stage;
    
    public GameObject startPopupPrefab;  // 여행 보내기 팝업 프리팹
    public GameObject stopPopupPrefab;  // 중단 팝업 프리팹
    public GameObject donePopupPrefab;   // 완료 팝업 프리팹
    public GameObject dim;

    public Transform popup;
    private GameObject currentPopupInstance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartTravelPopup()
    {
        // 여행 보내기 팝업 인스턴스 생성
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(startPopupPrefab);

    }

    public void StopTravelPopup()
    {
        // 중단 팝업 표시 로직 구현
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(stopPopupPrefab);
    }

    public void DoneTravelPopup()
    {
        // 완료 팝업 표시 로직 구현
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(donePopupPrefab);
    }

    // 팝업 닫기
    public void CloseActivePopup()
    {
        // 현재 팝업 인스턴스를 제거
        if (currentPopupInstance != null)
        {
            Destroy(currentPopupInstance);
            currentPopupInstance = null;
            dim.SetActive(false);
        }
    }

    private GameObject InstantiatePrefab(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is null.");
            return null;
        }

        return Instantiate(prefab, popup);
    }
}