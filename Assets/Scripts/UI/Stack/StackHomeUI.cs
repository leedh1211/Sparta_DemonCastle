
using UI.Stack;
using UnityEngine;
using UnityEngine.UI;

public class StackHomeUI : StackBaseUI
{
    Button startButton;
    Button exitButton;

    protected override StackUIState GetStackUIState()
    {
        return StackUIState.Home;
    }

    public override void Init(StackUIManager stackUiManager)
    {
        base.Init(stackUiManager);
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
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
