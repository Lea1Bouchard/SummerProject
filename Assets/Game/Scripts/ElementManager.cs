using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    private static ElementManager _instance;


    private Player player;
    public Sprite[] elementImages;
    public GameObject[] swordElementVFX;


    public static ElementManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Element Manager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        player.ElementChanged.AddListener(OnElementChange);

    }

    private void OnElementChange(Elements element)
    {

    }

    public Sprite ConvertElementToImage(Elements element)
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
                return null;
                break;
            default:
                return null;
        }
        return null;
    }
}
