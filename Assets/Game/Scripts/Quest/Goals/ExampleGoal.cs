using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGoal : Quest.QuestGoal
{
    public string Example;

    public override string GetDescription()
    {
        return $"Go fetch {Example}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<ExampleGameEvent>(OnExample);
    }

    private void OnExample(ExampleGameEvent eventInfo)
    {
        if (eventInfo.Example == Example)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
