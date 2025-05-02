using System;

namespace UI.Stack
{
    using TMPro;
    using UI.Stack;
    using UnityEngine;
    using UnityEngine.UI;

    public class StackGameOverUI : StackBaseUI
    {
        TextMeshProUGUI ScoreText;
        Button restartButton;
        Button exitButton;

        public override void Init(StackUIManager uiManager)
        {
            base.Init(uiManager);

            // 자식 오브젝트에서 컴포넌트 가져오기
            ScoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            restartButton = transform.Find("RestartButton").GetComponent<Button>();
            exitButton = transform.Find("ExitButton").GetComponent<Button>();

            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        protected override StackUIState GetStackUIState()
        {
            return StackUIState.Score;
        }

        // 점수 표시
        public void SetUI(int score)
        {
            Debug.Log("SetUI");
            if (score > PlayerPrefs.GetInt("MaxStack", 0))
            {
                PlayerPrefs.SetInt("MaxStack", score);
            }
            ScoreText.text = "Score: " + score.ToString();
        }

        void OnClickRestartButton()
        {
            stackUIManager.OnClickStart();
        }

        void OnClickExitButton()
        {
            stackUIManager.OnClickExit();
        }
    }

}