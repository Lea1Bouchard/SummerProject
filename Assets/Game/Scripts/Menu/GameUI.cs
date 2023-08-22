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
    public GameObject enemyBarHolder;
    public Image enemyHealthBar;
    public Image currentElementImage;
    public GameObject affinitiesHolder;
    public GameObject weaknessesHolder;
    public Sprite[] elementImages;

    // Start is called before the first frame update
    void Start()
    {
        enemyBarHolder.SetActive(false);
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
    }

    private void PlayerHealthPoint()
    {
        playerHealthBar.fillAmount = player.CurrenthealthPoints / player.MaxhealthPoints;
    }

    private void EnemyHealthPoint(GameObject enemy)
    {
        if (player.target != null)
            if (enemy.name == player.target.name)
            {
                EnemyController character = enemy.GetComponent<EnemyController>();
                enemyHealthBar.fillAmount = character.CurrenthealthPoints / character.MaxhealthPoints;
            }
    }

    private void OnTargetChange(Characters character)
    {
        if (character != null)
        {
            enemyBarHolder.SetActive(true);
            EnemyHealthPoint(character.gameObject);
            SetAffinitiesAndWeaknesses(character);
        }
        else
            enemyBarHolder.SetActive(false);
    }

    private void SetAffinitiesAndWeaknesses(Characters character)
    {
        ClearResistanceHolder();

        AddElementInList(character.Affinities, affinitiesHolder.transform);
        AddElementInList(character.Weaknesses, weaknessesHolder.transform);
    }

    private void ClearResistanceHolder()
    {
        for (int i = 0; i < affinitiesHolder.transform.childCount; i++)
        {
            Destroy(affinitiesHolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < weaknessesHolder.transform.childCount; i++)
        {
            Destroy(weaknessesHolder.transform.GetChild(i).gameObject);
        }
    }

    private void AddElementInList(List<Elements> elements, Transform parent)
    {
        foreach (Elements element in elements)
        {
            Sprite sprite = ConvertElementToImage(element);
            GameObject image = new GameObject();
            image.AddComponent<Image>().sprite = sprite;
            image.transform.SetParent(parent);
            image.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    
    private void OnElementChange(Elements element)
    {
        if (!currentElementImage.gameObject.activeSelf && element != Elements.Null)
            currentElementImage.gameObject.SetActive(true);

        currentElementImage.sprite = ConvertElementToImage(element);
    }

    private Sprite ConvertElementToImage(Elements element)
    {
        switch (element)
        {
            case Elements.Fire:
                return elementImages[0];
            case Elements.Water:
                return elementImages[1];
            case Elements.Air:
                return elementImages[2];
            case Elements.Earth:
                return elementImages[3];
            case Elements.Lightning:
                return elementImages[4];
            case Elements.Ice:
                return elementImages[5];
            case Elements.Light:
                return elementImages[6];
            case Elements.Darkness:
                return elementImages[7];
            case Elements.Null:
                currentElementImage.gameObject.SetActive(false);
                break;
            default:
                return null;
        }
        return null;
    }
}
