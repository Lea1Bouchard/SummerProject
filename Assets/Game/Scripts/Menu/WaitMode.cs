using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilityAI.Core;

public class WaitMode : MonoBehaviour
{
    private ElementManager elementManager;

    public GameObject enemyCanvas;
    public Slider enemyHealthBar;
    public GameObject affinitiesHolder;
    public GameObject weaknessesHolder;

    private List<EnemyController> enemies = new List<EnemyController>();
    private Camera mainCamera;
    private Plane[] cameraPlanes;
    private List<Collider> colliders = new List<Collider>();
    private List<GameObject> enemyBars = new List<GameObject>();

    private void OnEnable()
    {
        mainCamera = Camera.main;
        elementManager = ElementManager.Instance;

        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            colliders.Add(enemy.GetComponent<Collider>());
        }
        FindAllEnemies();
        SpawnEnemyBar();
    }

    private void OnDisable()
    {
        colliders.Clear();
        enemies.Clear();
        foreach (var bar in enemyBars)
        {
            Destroy(bar);
        }
        enemyBars.Clear();
    }

    private void FindAllEnemies()
    {
        foreach (var collider in colliders)
        {
            Bounds bounds = collider.bounds;
            if (GeometryUtility.TestPlanesAABB(cameraPlanes, bounds))//Get all enemies in camera view
            {
                if (collider.GetComponent<EnemyController>() && !enemies.Contains(collider.GetComponent<EnemyController>()))
                    enemies.Add(collider.GetComponent<EnemyController>());
            }
        }
    }

    private void SpawnEnemyBar()
    {
        foreach (var enemy in enemies)
        {
            //Find top of enemy
            Vector3 pos = new Vector3(enemy.transform.position.x, enemy.GetComponentInChildren<Renderer>().bounds.extents.y + 0.5f, enemy.transform.position.z);
            GameObject enemyBar = Instantiate(enemyCanvas, pos, 
                Quaternion.LookRotation(mainCamera.transform.forward));
            enemyHealthBar = enemyBar.transform.Find("HealthBar").GetComponent<Slider>();
            weaknessesHolder = enemyBar.transform.Find("WeaknessHolder").gameObject;
            affinitiesHolder = enemyBar.transform.Find("AffinitiesHolder").gameObject;

            AddElementInList(enemy.Affinities, affinitiesHolder.transform);
            AddElementInList(enemy.Weaknesses, weaknessesHolder.transform);

            enemyBars.Add(enemyBar);
        }
    }

    private void AddElementInList(List<Elements> elements, Transform parent)
    {
        foreach (Elements element in elements)
        {
            Sprite sprite = elementManager.ConvertElementToImage(element);
            GameObject image = new GameObject();
            image.AddComponent<Image>().sprite = sprite;
            image.transform.SetParent(parent);
            RectTransform rect = image.GetComponent<RectTransform>();
            rect.localScale = new Vector3(1, 1, 1);
            rect.anchoredPosition3D = new Vector3(rect.position.x, rect.position.y, 0);
            rect.sizeDelta = new Vector2(25, 25);

        }
    }
}
