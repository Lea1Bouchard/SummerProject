using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchGoal : Quest.QuestGoal
{
    public string FetchedItem;

    public override string GetDescription()
    {
        return $"Go fetch the {FetchedItem}";
    }

    public override void Initialize()
    {
        base.Initialize();
        goalType = Enums.GoalType.Fetch;
        EventManager.Instance.AddListener<FetchGameEvent>(OnFetched);
    }

    private void OnFetched(FetchGameEvent eventInfo)
    {
        if (eventInfo.FetchedItem == FetchedItem)
        {
            currentAmount++;
            Evaluate();
        }
    }
}
