using Dialogue;
using UI;
using UnityEngine.SceneManagement;

namespace Manager.DialogueFlow
{
    public class ConstructorDialogueFlow : DialogueFlow
    {
        public ConstructorDialogueFlow(NpcDialogueData data) : base(data) {}
        
        public override void ProcessOption(string option)
        {
            if (option == "예")
            {
                SceneManager.LoadScene("StackGameScene");
            }
            else
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}