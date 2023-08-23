using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayGoal : Quest.QuestGoal
{
    public Enums.EnemyType KilledEnemie;

    public override string GetDescription()
    {
        return $"Slay {this.requiredAmount} {TypeTranslate.Instance.TranslateEnemies(KilledEnemie)}";
    }

    public override void Initialize()
    {
        base.Initialize();
        goalType = Enums.GoalType.Slay;
        EventManager.Instance.AddListener<KillGameEvent>(OnKill);
    }

    private void OnKill(KillGameEvent eventInfo)
    {
        if (eventInfo.KilledEnemie == KilledEnemie)
        {
            currentAmount++;
            Evaluate();
        }
    }
}
