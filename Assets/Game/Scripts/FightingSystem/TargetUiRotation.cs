using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUiRotation : MonoBehaviour
{
    #region variable
    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    private Transform targetLocation;
    private Player player;
    #endregion

    private void Awake()
    {
        player = Player.Instance;
    }

    private void OnEnable()
    {
        targetLocation = player.target.targetLocation;
        StartCoroutine(LookAtCamera());
    }

    //Using an IEnumerator to simulate an update that won't run when we don't want it to
    //Rotates to look at player every frame
    private IEnumerator LookAtCamera()
    {
        while (gameObject.activeInHierarchy)
        {
            this.gameObject.transform.position = targetLocation.position;

            this.gameObject.transform.LookAt(lockOnCamera.transform.position);

            yield return null;
        }
    }
}
