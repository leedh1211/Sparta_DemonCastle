using Dialogue;
using UI;

namespace Manager.DialogueFlow
{
    public class DefaultDialogueFlow : DialogueFlow
    {
        public DefaultDialogueFlow(NpcDialogueData data) : base(data) {}

        
        public override void ProcessOption(string option)
        {
            if (option == "계속하기")
            {
                DialogueManager.Instance.ContinueDialogue();
            }
            else
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}