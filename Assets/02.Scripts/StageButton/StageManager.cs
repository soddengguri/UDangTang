using System.Collections;
using System.Collections.Generic;
using Container.Popup;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform ContentContainer;

    string[] StageLogoImageName = new string[]
    {
         "StageLogo00", "StageLogo01"

                        ,"StageLogo02", "StageLogo03", "StageLogo04", "StageLogo05", "StageLogo06", "StageLogo07", "StageLogo08"

   };


    void Start()
    {

        // Assets > Resources 폴더에 있는 Stage 프리팹을 로드
        GameObject stagePrefab = Resources.Load<GameObject>("Stage");

        // 로드한 프리팹을 이용해 Scroll View 에 들어갈 10개의 스테이지 항목 생성
        for (int i = 0; i < 10; i++)
        {
            // Instantiate 함수는 객체를 생성하는 함수입니다.
            Transform stage = Instantiate(stagePrefab).transform;

            // GetComponent 는 객체에 있는 컴포넌트 중 < > 사이에 입력된 컴포넌트를 가져옵니다.
            // 만약 해당 컴포넌트가 없다면 null 이 리턴됩니다.
            Stage stageTrip = stage.GetComponent<Stage>();

            // Stage 스크립트에 있는 Init 함수를 호출합니다.
            stageTrip.Init(i, this);

            /*
            Image image = stage.GetComponent<Image>();
            Sprite btnsprite = Resources.Load<Sprite>(StageLogoImageName[i]);
            image.sprite = btnsprite;
            */
        }
    }

    public void OnStageClicked(Stage stageTrip)
    {
        // 선택된 StageItem 에서 이 함수를 호출합니다.
        // 파라미터로 넘어온 stageItem 을 통해 어떤 스테이지가 선택되었는지 알 수 있습니다.
    }

    void Update()
    {

    }
}
