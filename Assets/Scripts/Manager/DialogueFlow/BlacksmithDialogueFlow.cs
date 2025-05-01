using Dialogue;
using UI;
using UnityEngine;

namespace Manager.DialogueFlow
{
    public class BlacksmithDialogueFlow : DialogueFlow
    {
        public BlacksmithDialogueFlow(NpcDialogueData data) : base(data) {}
        private int attackSpeed = PlayerPrefs.GetInt("AttackSpeed", 0);
        int ProjectileCount = PlayerPrefs.GetInt("ProjectileCount", 0);
        int Power = PlayerPrefs.GetInt("Power", 0);

        public override void ProcessOption(string option)
        {
            if (option == "강화한다")
            {
                DialogueManager.Instance.ContinueDialogue();
            }
            else if (option == "공격속도 증가")
            {
                PlayerPrefs.SetInt("AttackSpeed", attackSpeed + 1);
                DialogueUI.Instance.HideDialogue();
            }
            else if (option == "발사체 개수 증가")
            {
                PlayerPrefs.SetInt("ProjectileCount", ProjectileCount + 1);
                DialogueUI.Instance.HideDialogue();
            }
            else if (option == "공격력 강화")
            {
                PlayerPrefs.SetInt("Power", Power + 1);
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

        public void checkLevelUpCount()
        {
            int MaxWave = PlayerPrefs.GetInt("MaxWave", 0);
            if (MaxWave >= attackSpeed + ProjectileCount + Power)
            {
                DialogueManager.Instance.ContinueDialogue(2);
            }
        }
    }
}