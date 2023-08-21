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

        Debug.Log(characters);
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].HealthChanged.AddListener(OnHealthChange);
        }

        player.ElementChanged.AddListener(OnElementChange);
    }

    private void OnHealthChange(GameObject gameObject)
    {
        if (gameObject.GetComponent<Player>())
            PlayerHealthPoint();
        else if (gameObject.GetComponent<EnemyController>())
            Debug.Log("Enemy health Changed");
        else
            Debug.Log("health changed but not player or enemy");
    }

    private void OnElementChange(Elements element)
    {
        if(!currentElementImage.gameObject.activeSelf && element != Elements.Null)
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

    private void PlayerHealthPoint()
    {
        playerHealthBar.fillAmount = Player.Instance.CurrenthealthPoints / Player.Instance.MaxhealthPoints;
    }
}
