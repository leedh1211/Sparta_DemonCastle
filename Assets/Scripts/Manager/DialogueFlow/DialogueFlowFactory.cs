using System;
using System.Collections.Generic;
using Dialogue;
using UI;
using UnityEngine;

namespace Manager.DialogueFlow
{
    public static class DialogueFlowFactory
    {
        private static Dictionary<string, Func<NpcDialogueData, DialogueFlow>> flowMap =
            new Dictionary<string, Func<NpcDialogueData, DialogueFlow>>()
            {
                { "Blacksmith", data => new BlacksmithDialogueFlow(data) },
                { "Architect", data => new DefaultDialogueFlow(data) },
                { "Gatekeeper", data => new DefaultDialogueFlow(data) }
            };

        public static DialogueFlow GetFlow(string npcName, NpcDialogueData data)
        {
            if (flowMap.TryGetValue(npcName, out var constructor))
            {
                return constructor(data);
            }

            // 기본 흐름 반환
            return new DefaultDialogueFlow(data);
        }
    }
}