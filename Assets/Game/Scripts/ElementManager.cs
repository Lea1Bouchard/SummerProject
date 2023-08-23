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
}
