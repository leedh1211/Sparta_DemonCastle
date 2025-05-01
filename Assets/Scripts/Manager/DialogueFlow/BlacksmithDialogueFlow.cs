using Dialogue;
using UI;
using UnityEngine;

namespace Manager.DialogueFlow
{
    public class BlacksmithDialogueFlow : DialogueFlow
    {
        public BlacksmithDialogueFlow(NpcDialogueData data) : base(data) {}

        public override void ProcessOption(string option)
        {
            if (option == "강화한다")
            {
                DialogueManager.Instance.ContinueDialogue();
            }
            else if (option == "공격속도 증가")
            {
                DialogueUI.Instance.HideDialogue();
            }
            else if (option == "발사체 개수 증가")
            {
                DialogueUI.Instance.HideDialogue();
            }
            else if (option == "공격력 강화")
            {
                DialogueUI.Instance.HideDialogue();
            }
            else if (option == "그만둔다")
            {
                DialogueManager.Instance.EndDialogue();
            }
            else
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}