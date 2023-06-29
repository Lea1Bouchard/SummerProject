using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkGoal : Quest.QuestGoal
{
    public string TalkedTo;

    public override string GetDescription()
    {
        return $"Talk to {TalkedTo}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<TalkGameEvent>(OnTalk);
    }

    private void OnTalk(TalkGameEvent eventInfo)
    {
        if (eventInfo.TalkedTo == TalkedTo)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
