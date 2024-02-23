using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using ExcelDataReader;

public class StageManager : MonoBehaviour
{
    public string excelFilePath = "Assets/05.Excel/ArrivalTime.xlsx";
    public string currentTrip;
    public Transform ContentContainer;
    Stage stageTrip = null;

    string[] StageLogoImageName = new string[]
    {
         "StageLogo00", "StageLogo01", "StageLogo02", "StageLogo03", "StageLogo04", "StageLogo05", "StageLogo06", "StageLogo07", "StageLogo08"
    };

    public void Start()
    {
        LoadStages();
    }

    private void LoadStages()
    {
        DataTable dataTable = LoadExcelDataTable();
        // Assets > Resources 폴더에 있는 Stage 프리팹을 로드
        GameObject stagePrefab = Resources.Load<GameObject>("Stage");

        for (int i = 0; i < 10; i++)
        {
            Transform stage = Instantiate(stagePrefab, ContentContainer).transform;
            stageTrip = stage.GetComponent<Stage>();
            stageTrip.Init(i, this);
        }
    }

    private DataTable LoadExcelDataTable()
    {
        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                return dataSet.Tables[0];
            }
        }
    }

    void Update()
    {
        
    }

    public void OnStageClicked(Stage clickedStage)
    {
        // 선택된 StageItem 에서 해당 함수 호출
        // 파라미터로 넘어온 stageItem 을 통해 어떤 스테이지가 선택되었는지 알 수 있다.
        currentTrip = clickedStage.txtStageName.text;

        // 로그를 출력하여 확인
        Debug.Log("Current Trip: " + currentTrip);

        // TxtStageTime 업데이트
        UpdateTxtStageTime();

        // 현재 클릭된 스테이지만 활성화
        DisableOtherStageButtons(clickedStage);
    }

    public void DisableOtherStageButtons(Stage clickedStage)
    {
        foreach (Transform stageTransform in ContentContainer)
        {
            Stage stage = stageTransform.GetComponent<Stage>();

            // 현재 클릭된 스테이지를 제외하고 나머지 버튼 비활성화
            if (stage != null && stage != clickedStage)
            {
                Button stageButton = stage.button;
                if (stageButton != null)
                    stageButton.enabled = false;
            }
        }
    }

    public void EnableAllStageButtons()
    {
        foreach (Transform stageTransform in ContentContainer)
        {
            Stage stage = stageTransform.GetComponent<Stage>();

            // 모든 스테이지 버튼 활성화
            if (stage != null)
            {
                Button stageButton = stage.button;
                if (stageButton != null)
                    stageButton.enabled = true;
            }
        }
    }

    public void UpdateTxtStageTime()
    {
        DataTable dataTable = LoadExcelDataTable();
        int columnIndex = GetColumnIndex(dataTable, currentTrip);

        foreach (Transform stageTransform in ContentContainer)
        {
            Stage stage = stageTransform.GetComponent<Stage>();

            // currentTrip의 값에 따라 TxtStageTime.text 변경
            if (stage != null)
            {
                string stageName = stage.txtStageName.text;

                if (columnIndex != -1)
                {
                    // 현재 스테이지의 TxtStageTime.text 값을 Excel 열에 해당하는 값으로 설정
                    string timeCellValue = GetExcelCellValue(dataTable, columnIndex, stageName) + "초";
                    stage.txtStageTime.text = timeCellValue;
                }
            }
        }
    }

    // Excel 데이터에서 해당 스테이지의 열 인덱스 찾기
    private int GetColumnIndex(DataTable dataTable, string currentTrip)
    {
        // 열 인덱스 찾기
        for (int col = 1; col < dataTable.Columns.Count; col++)
        {
            if (dataTable.Rows[0][col].ToString() == currentTrip)
            {
                return col;
            }
        }

        // 해당 스테이지가 없는 경우 -1 반환
        return -1;
    }

    // Excel 데이터에서 특정 셀의 값을 가져오기
    private string GetExcelCellValue(DataTable dataTable, int columnIndex, string stageName)
    {
        int rowIndex = GetRowIndexOfStage(dataTable, stageName);

        // 엑셀에서 해당 스테이지의 시간 데이터 가져오기
        string timeCellValue = (rowIndex != -1) ? dataTable.Rows[rowIndex][columnIndex].ToString() : "";
        return timeCellValue;
    }

    // Excel 데이터에서 특정 스테이지의 행 인덱스 찾기
    private int GetRowIndexOfStage(DataTable dataTable, string stageName)
    {
        // 행 인덱스 찾기
        for (int row = 1; row < dataTable.Rows.Count; row++)
        {
            if (dataTable.Rows[row][0].ToString() == stageName)
            {
                return row;
            }
        }

        // 해당 스테이지가 없는 경우 -1 반환
        return -1;
    }
}