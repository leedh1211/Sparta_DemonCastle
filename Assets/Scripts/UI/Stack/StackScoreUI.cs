
using TMPro;
using UI.Stack;
using UnityEngine;
using UnityEngine.UI;

public class StackScoreUI : StackBaseUI
{
    TextMeshProUGUI StackText;
    Button startButton;
    Button exitButton;

    public override void Init(StackUIManager uiManager)
    {
        base.Init(uiManager);
        StackText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override StackUIState GetStackUIState()
    {
        return StackUIState.Score;
    }

    public void SetUI(int score)
    {
        StackText.text = score.ToString();
    }

    void OnClickStartButton()
    {
        stackUIManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        stackUIManager.OnClickExit();
    }
}
