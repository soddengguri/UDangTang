using System.ComponentModel;
using System;

namespace Container.Popup
{
    /// <summary>
    /// Description 이 버튼의 텍스트로 들어감.
    /// </summary>
    [Serializable]
    public enum PopupButtonType
    {

        None = 0,
        [Description("예")]
        Yes = 1,
        [Description("아니오")]
        No = 2,
        [Description("확인")]
        Confirm = 3,
        [Description("닫기")]
        Close = 4,
        [Description("다시하기")]
        RePlay = 5,
        [Description("홈으로")]
        GoHome = 6,
        [Description("게임종료")]
        FinishGame = 7,
        [Description("시작")]
        Start = 8
    }
}