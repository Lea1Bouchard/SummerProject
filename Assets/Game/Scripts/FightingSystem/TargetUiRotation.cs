using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUiRotation : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    private Transform targetLocation;
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    private void OnEnable()
    {
        targetLocation = player.target.targetLocation;
        StartCoroutine(LookAtCamera());
    }

    private IEnumerator LookAtCamera()
    {
        while(gameObject.activeInHierarchy)
        {
            this.gameObject.transform.position = targetLocation.position;

            this.gameObject.transform.LookAt(lockOnCamera.transform.position);

            yield return null;
        }
    }
}
