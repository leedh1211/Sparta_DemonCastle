using Dialogue;

namespace Manager.DialogueFlow
{
    public abstract class DialogueFlow
    {
        protected NpcDialogueData npcData;

        public DialogueFlow(NpcDialogueData data)
        {
            npcData = data;
        }

        public abstract void ProcessOption(string option);
    }
}