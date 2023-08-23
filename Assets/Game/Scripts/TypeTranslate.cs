using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to translate types to usable strings
public class TypeTranslate : MonoBehaviour
{
    [HideInInspector] public Dictionary<Enums.EnemyType, string> enemyNames;

    private static TypeTranslate _instance;

    public static TypeTranslate Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("translate is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        InitializeDictionaries();
    }
    //Returns the string linked to the enemy type
    //to be used in a written line (dialogue or quest)
    public string TranslateEnemies(Enums.EnemyType Translated)
    {
        if (enemyNames.ContainsKey(Translated))
            return enemyNames[Translated];

        return null;
    }

    private void InitializeDictionaries()
    {
        enemyNames = new Dictionary<Enums.EnemyType, string>()
        {
            { Enums.EnemyType.Tree_Leaf, "Razor leaf" },
            { Enums.EnemyType.Tree_Spike, "Spike plant" },
            { Enums.EnemyType.Tree_Mushroom, "Glow shroom" },
            { Enums.EnemyType.Tree_Coral, "Hull rider" },
            { Enums.EnemyType.Scarab, "Scarab" },
            { Enums.EnemyType.Golem, "Golem" },
            { Enums.EnemyType.Dragon_NoWings, "Reptord" },
            { Enums.EnemyType.Dragon_Chunky, "Gorgon" },
            { Enums.EnemyType.Dragon_Normal, "Dragon" },
            { Enums.EnemyType.Dragon_Long, "Salamander" }
        };
    }
}
