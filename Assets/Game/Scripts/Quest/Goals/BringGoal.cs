using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringGoal : Quest.QuestGoal
{
    public string BroughtItem;
    public string Location;

    public override string GetDescription()
    {
        return $"Bring the {BroughtItem} to the {Location}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<BringGameEvent>(OnBrought);
    }

    private void OnBrought(BringGameEvent eventInfo)
    {
        if (eventInfo.BroughtItem == BroughtItem && eventInfo.Location == Location)
        {
            currentAmount++;
            Evaluate();
        }
    }
}
