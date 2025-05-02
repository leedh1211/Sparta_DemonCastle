
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Stack
{
    public enum StackUIState { Home, Game, Score }

    public class StackUIManager : MonoBehaviour
    {
        static StackUIManager instance;
        public static StackUIManager Instance => instance;

        StackUIState currentState = StackUIState.Home;
        StackHomeUI stackHomeUI = null;
        StackGameUI stackGameUI = null;
        StackScoreUI _stackScoreUI = null;
        TheStack theStack = null;
        StackGameOverUI stackGameOverUI = null;

        public StackGameUI StackGameUI => stackGameUI;
        public StackScoreUI StackScoreUI => _stackScoreUI;
        public StackGameOverUI StackGameOverUI => stackGameOverUI;

        private void Awake()
        {
            instance = this;
            theStack = FindObjectOfType<TheStack>();

            stackHomeUI = GetComponentInChildren<StackHomeUI>(true);
            stackHomeUI?.Init(this);

            stackGameUI = GetComponentInChildren<StackGameUI>(true);
            stackGameUI?.Init(this);

            stackGameOverUI = GetComponentInChildren<StackGameOverUI>(true);
            stackGameOverUI?.Init(this);

            ChangeState(StackUIState.Home);
        }

        public void ChangeState(StackUIState state)
        {
            currentState = state;
            stackHomeUI?.SetActive(currentState);
            stackGameUI?.SetActive(currentState);
            stackGameOverUI?.SetActive(currentState);
        }

        public void OnClickStart()
        {
            theStack.Restart();
            ChangeState(StackUIState.Game);
        }

        public void OnClickExit()
        {
            SceneManager.LoadScene("MainHub");
        }
    }
}
