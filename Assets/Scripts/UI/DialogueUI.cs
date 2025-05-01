using System.Text.RegularExpressions;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI npcNameText;
        [SerializeField] private Transform optionsBox;
        [SerializeField] private GameObject optionButtonPrefab;

        public static DialogueUI Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void ShowDialogue(string npcName, DialogueEntry entry)
        {
            if (!dialoguePanel.activeSelf)
                dialoguePanel.SetActive(true);

            npcNameText.text = npcName;
            dialogueText.text = ReplaceRecordVariables(entry.text);

            ClearOptions();

            if (entry.options != null && entry.options.Count > 0)
            {
                for (int i = 0; i < entry.options.Count; i++)
                {
                    CreateOptionButton(entry.options[i], i == 0);
                }
            }
            else
            {
                // 선택지 없으면 계속하기 버튼
                CreateOptionButton(new OptionData { text = "계속하기", category = DialogueCategory.Conversation }, true);
            }
        }

        public void HideDialogue()
        {
            dialoguePanel.SetActive(false);
        }

        public bool IsDialogueActive() => dialoguePanel.activeSelf;

        private void CreateOptionButton(OptionData option, bool setAsFirst)
        {
            GameObject optionButtonObj = Instantiate(optionButtonPrefab, optionsBox);
            Button button = optionButtonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = optionButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = option.text;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnOptionSelected(option));

            if (setAsFirst)
                EventSystem.current.SetSelectedGameObject(optionButtonObj);
        }

        private void ClearOptions()
        {
            foreach (Transform child in optionsBox)
                Destroy(child.gameObject);
        }

        private void OnOptionSelected(OptionData option)
        {
            if (option.text == "계속하기")
                DialogueManager.Instance.ContinueDialogue();
            else
                DialogueManager.Instance.HandleOption(option.text, option.category);
        }
        
        
        private string ReplaceRecordVariables(string text)
        {
            // {변수명} 형태를 모두 찾아서 처리
            return Regex.Replace(text, @"\{(.*?)\}", match =>
            {
                string key = match.Groups[1].Value;

                // PlayerPrefs에서 값을 가져옴 (기본값 0)
                int value = PlayerPrefs.GetInt(key, 0);

                // 값을 문자열로 치환
                return value.ToString();
            });
        }
    }
}
