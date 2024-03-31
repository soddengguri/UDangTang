using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using ExcelDataReader;

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

    // 조각 지급 수 데이터를 저장하는 딕셔너리
    private Dictionary<string, int> fragmentData = new Dictionary<string, int>();

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

    // Start is called before the first frame update
    void Start()
    {
        LoadFragmentDataFromExcel();
    }

    public void StartTravelPopup(string stageName)
    {
        // 여행 보내기 팝업 인스턴스 생성
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(startPopupPrefab);

        // 팝업 텍스트 설정
        SetPopupText(stageName);

    }

    public void StopTravelPopup(string stageName)
    {
        // 중단 팝업 표시 로직 구현
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(stopPopupPrefab);

        // 팝업 텍스트 설정
        SetPopupText(stageName);
    }

    public void DoneTravelPopup(string stageName)
    {
        // 완료 팝업 표시 로직 구현
        CloseActivePopup();
        dim.SetActive(true);
        currentPopupInstance = InstantiatePrefab(donePopupPrefab);

        // 팝업 텍스트 설정
        SetPopupText(stageName);
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

    // 엑셀에서 조각 지급 수 데이터 읽어오기
    private void LoadFragmentDataFromExcel()
    {
        string excelFilePath = Path.Combine(Application.dataPath, "05.Excel/Pieces.xlsx");

        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                var dataTable = dataSet.Tables[0];

                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    string stageName = dataTable.Rows[i][0].ToString();
                    int fragmentCount = int.Parse(dataTable.Rows[i][1].ToString());
                    fragmentData.Add(stageName, fragmentCount);
                }
            }
        }
    }


    // 팝업 텍스트 설정
    private void SetPopupText(string stageName)
    {
        if (fragmentData.ContainsKey(stageName))
        {
            int fragments = fragmentData[stageName];

            // 현재 팝업 인스턴스에서 Pieces(basic) 이름을 가진 Text 오브젝트 찾기
            Transform piecesTextTransform = currentPopupInstance.transform.Find("Pieces(basic)");

            if (piecesTextTransform != null)
            {
                Text txtPieces = piecesTextTransform.GetComponent<Text>();

                if (txtPieces != null)
                {
                    // Pieces(basic) 텍스트 오브젝트의 값을 변경
                    txtPieces.text = fragments.ToString();
                }
                else
                {
                    Debug.LogError("Text component not found in Pieces(basic) object.");
                }
            }
            else
            {
                Debug.LogError("Pieces(basic) object not found in popup prefab.");
            }
        }
        else
        {
            Debug.LogError($"Fragment data not found for stage: {stageName}");
        }
    }



    // 조각 지급 수 데이터 설정
    public void SetFragmentData(Dictionary<string, int> data)
    {
        fragmentData = data;
    }
}