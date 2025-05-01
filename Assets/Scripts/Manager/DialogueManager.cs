using System.Collections.Generic;
using Dialogue;
using Manager.DialogueFlow;
using Newtonsoft.Json;
using UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private List<NpcDialogueData> npcDialogueList;
    private NpcDialogueData currentNPC;
    private int currentDialogueIndex = 0;
    private DialogueFlow currentFlow;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadDialogueData();
    }

    private void LoadDialogueData()
    {
        TextAsset dialogueJson = Resources.Load<TextAsset>("DialogueData/NPCConversation");

        if (dialogueJson == null)
        {
            return;
        }

        DialogueDataWrapper wrapper = JsonConvert.DeserializeObject<DialogueDataWrapper>(dialogueJson.text);

        if (wrapper == null || wrapper.items == null)
        {
            return;
        }

        npcDialogueList = wrapper.items;
        
    }

    public void StartDialogue(string npcName)
    {
        currentDialogueIndex = 0;
        currentNPC = npcDialogueList.Find(npc => npc.npcName == npcName);
        currentFlow = DialogueFlowFactory.GetFlow(npcName, currentNPC);
        ShowDialogueByKey("Intro");
    }

    private void ShowDialogueByKey(string key)
    {
        var entry = currentNPC.dialogues.Find(d => d.key == key)?.value;
        if (entry != null)
            DialogueUI.Instance.ShowDialogue(currentNPC.npcName, entry);
    }

    public void ContinueDialogue()
    {
        currentDialogueIndex++;
        string nextKey = "Conversation" + currentDialogueIndex;
        var entry = currentNPC.dialogues.Find(d => d.key == nextKey)?.value;

        if (entry != null)
            DialogueUI.Instance.ShowDialogue(currentNPC.npcName, entry);
        else
            EndDialogue();
    }

    public void EndDialogue()
    {
        ShowDialogueByKey("End");
    }

    public void HandleOption(string selectedOption, DialogueCategory category)
    {
        if (category == DialogueCategory.End)
        {
            DialogueUI.Instance.HideDialogue();
            return;
        }

        currentFlow.ProcessOption(selectedOption);
    }
}
