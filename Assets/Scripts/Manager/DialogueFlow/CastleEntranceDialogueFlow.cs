using Dialogue;
using UI;
using UnityEngine.SceneManagement;

namespace Manager.DialogueFlow
{
    public class CastleEntranceDialogueFlow : DialogueFlow
    {
        public CastleEntranceDialogueFlow(NpcDialogueData data) : base(data) {}
        
        public override void ProcessOption(string option)
        {
            if (option == "이동한다")
            {
                SceneManager.LoadScene("MainGameScene");
            }
            else
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}