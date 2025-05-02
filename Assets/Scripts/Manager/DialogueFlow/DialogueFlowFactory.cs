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
                { "Constructor", data => new ConstructorDialogueFlow(data) },
                { "CastleEntrance", data => new CastleEntranceDialogueFlow(data) },
                { "RecordNpc", data => new RecordNpcDialogueFlow(data) }
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