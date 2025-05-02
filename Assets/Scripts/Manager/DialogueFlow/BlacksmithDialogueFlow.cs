using System;
using Dialogue;
using UI;
using UnityEngine;

namespace Manager.DialogueFlow
{
    public class BlacksmithDialogueFlow : DialogueFlow
    {
        public BlacksmithDialogueFlow(NpcDialogueData data) : base(data) {}
        private int attackSpeed = PlayerPrefs.GetInt("AttackSpeed", 0);
        private int ProjectileCount = PlayerPrefs.GetInt("ProjectileCount", 0);
        private int Power = PlayerPrefs.GetInt("Power", 0);

        public override void ProcessOption(string option)
        {
            if (option == "강화한다")
            {
                DialogueManager.Instance.ContinueDialogue();
            }
            else if (option == "공격속도 증가")
            {
                if (checkLevelUpCount())
                {
                    PlayerPrefs.SetInt("AttackSpeed", attackSpeed + 1);
                    DialogueManager.Instance.ContinueDialogue(3);    
                }
            }
            else if (option == "발사체 개수 증가")
            {
                if (checkLevelUpCount())
                {
                    checkLevelUpCount();
                    PlayerPrefs.SetInt("ProjectileCount", ProjectileCount + 1);
                    DialogueManager.Instance.ContinueDialogue(3);
                }
            }
            else if (option == "공격력 강화")
            {
                if (checkLevelUpCount())
                {
                    checkLevelUpCount();
                    PlayerPrefs.SetInt("Power", Power + 1);
                    DialogueManager.Instance.ContinueDialogue(3);
                }
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

        public bool checkLevelUpCount()
        {
            int MaxWave = PlayerPrefs.GetInt("MaxWave", 0);
            int currentLevel = attackSpeed + ProjectileCount + Power;
            if (currentLevel >= MaxWave)
            {
                Debug.Log("현재레벨"+currentLevel);
                Debug.Log("현재웨이브"+MaxWave);
                DialogueManager.Instance.ContinueDialogue(2);
                return false;
            }

            return true;
        }
    }
}