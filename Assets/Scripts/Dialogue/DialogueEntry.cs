using System;
using System.Collections.Generic;

namespace Dialogue
{
    [Serializable]
    public class DialogueEntry
    {
        public string text;
        public DialogueCategory category; // 이 대사의 category
        public List<OptionData> options;   // 선택지 (옵션별 category 포함)
    }

    [Serializable]
    public class OptionData
    {
        public string text;
        public DialogueCategory category;
    }
}