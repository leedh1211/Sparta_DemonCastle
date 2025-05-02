
using UI.Stack;
using UnityEngine;

namespace UI.Stack
{
    public abstract class StackBaseUI : MonoBehaviour
    {
        protected StackUIManager stackUIManager;

        public virtual void Init(StackUIManager uiManager)
        {
            this.stackUIManager = uiManager;
        }

        protected abstract StackUIState GetStackUIState();

        public void SetActive(StackUIState state)
        {
            this.gameObject.SetActive(GetStackUIState() == state);
        }
    }
}
