using Dialogue;
using UI;
using UnityEngine;

namespace Manager.DialogueFlow
{
    public class RecordNpcDialogueFlow : DialogueFlow
    {
        public RecordNpcDialogueFlow(NpcDialogueData data) : base(data) {}
        
        public override void ProcessOption(string option)
        {
            if (option == "Wave 기록 확인")
            {
                DialogueManager.Instance.ContinueDialogue();
            }
            else if (option == "Stack 기록 확인")
            {
                DialogueManager.Instance.ContinueDialogue(2);
                
            }else if (option == "게임초기화")
            {
                PlayerPrefs.DeleteAll();
                DialogueManager.Instance.EndDialogue();
            }
            else
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}