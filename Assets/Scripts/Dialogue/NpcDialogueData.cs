using System;
using System.Collections.Generic;

namespace Dialogue
{
    [Serializable]
    public class NpcDialogueData
    {
        public string npcName;
        public List<DialogueKeyValue> dialogues;
    }
}