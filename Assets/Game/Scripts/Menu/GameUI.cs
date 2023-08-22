using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;
using UnityEngine.UI;
using Enums;
using System;

public class GameUI : MonoBehaviour
{
    private Characters[] characters;
    private Player player;
    public Image playerHealthBar;
    public GameObject enemyHealthBarHolder;
    public Image enemyHealthBar;
    public Image currentElementImage;
    public Sprite[] elementImages;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBarHolder.SetActive(false);
        currentElementImage.gameObject.SetActive(false);

        characters = FindObjectsOfType<Characters>();
        player = Player.Instance;

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].HealthChanged.AddListener(OnHealthChange);
        }

        player.ElementChanged.AddListener(OnElementChange);
        player.TargetChanged.AddListener(OnTargetChange);
    }

    private void OnHealthChange(GameObject gameObject)
    {
        if (gameObject.GetComponent<Player>())
            PlayerHealthPoint();
        else if (gameObject.GetComponent<EnemyController>())
            EnemyHealthPoint(gameObject);
        else
            Debug.Log("health changed but not player or enemy");
    }

    private void PlayerHealthPoint()
    {
        playerHealthBar.fillAmount = player.CurrenthealthPoints / player.MaxhealthPoints;
    }

    private void EnemyHealthPoint(GameObject enemy)
    {
        Debug.Log(enemy.name + " health changed");
        if (player.target != null)
            if (enemy.name == player.target.name)
            {
                EnemyController character = enemy.GetComponent<EnemyController>();
                enemyHealthBar.fillAmount = character.CurrenthealthPoints / character.MaxhealthPoints;
                Debug.Log("Target Enemy health changed");
            }
    }

    private void OnTargetChange(Characters character)
    {
        if (character != null)
        {
            enemyHealthBarHolder.SetActive(true);
            EnemyHealthPoint(character.gameObject);
            Debug.Log(character + " health showed");
        }
        else
            enemyHealthBarHolder.SetActive(false);
    }

    private void OnElementChange(Elements element)
    {
        if (!currentElementImage.gameObject.activeSelf && element != Elements.Null)
            currentElementImage.gameObject.SetActive(true);

        switch (element)
        {
            case Elements.Fire:
                currentElementImage.sprite = elementImages[0];
                break;
            case Elements.Water:
                currentElementImage.sprite = elementImages[1];
                break;
            case Elements.Air:
                currentElementImage.sprite = elementImages[2];
                break;
            case Elements.Earth:
                currentElementImage.sprite = elementImages[3];
                break;
            case Elements.Lightning:
                currentElementImage.sprite = elementImages[4];
                break;
            case Elements.Ice:
                currentElementImage.sprite = elementImages[5];
                break;
            case Elements.Light:
                currentElementImage.sprite = elementImages[6];
                break;
            case Elements.Darkness:
                currentElementImage.sprite = elementImages[7];
                break;
            case Elements.Null:
                currentElementImage.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
