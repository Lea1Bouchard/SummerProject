using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Characters[] characters;
    public Image PlayerHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        characters = FindObjectsOfType<Characters>();
        Debug.Log(characters);
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].HealthChanged.AddListener(OnHealthChanged);
        }
    }

    private void Update()
    {
        ChangePlayerHealth();
    }

    private void OnHealthChanged(GameObject gameObject)
    {
        if (gameObject.GetComponent<Player>())
            PlayerHealthPoint();
        else if (gameObject.GetComponent<EnemyController>())
            Debug.Log("Enemy health Changed");
        else
            Debug.Log("health changed but not player or enemy");
    }

    private void PlayerHealthPoint()
    {
        PlayerHealthBar.fillAmount = Player.Instance.CurrenthealthPoints / Player.Instance.MaxhealthPoints;
    }

    public void ChangePlayerHealth()
    {
        Player.Instance.ReceiveDamage(Enums.Elements.Fire, 1);
    }
}
