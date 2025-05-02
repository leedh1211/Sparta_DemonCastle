
using TMPro;
using UI.Stack;
using UnityEngine;

public class StackGameUI : StackBaseUI
{
    TextMeshProUGUI StackScoreText;

    protected override StackUIState GetStackUIState()
    {
        return StackUIState.Game;
    }

    public override void Init(StackUIManager uiManager)
    {
        base.Init(uiManager);
        StackScoreText = transform.Find("StackText").GetComponent<TextMeshProUGUI>();
    }

    public void SetUI(int score)
    {
        StackScoreText.text = score.ToString();
    }
}
