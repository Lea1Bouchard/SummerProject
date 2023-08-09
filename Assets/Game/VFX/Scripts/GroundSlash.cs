using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public float speed = 30;
    public float slowDownRate = 0.01f;
    public float detectingDistance = 0.1f;
    public float destroyDelay = 5;

    private Rigidbody rb;
    private bool isStopped;
    private Vector3 destination;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SlowDown());
        }
        else
            Debug.Log("No RigidBody on GroundSlash");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStopped)
        {
            MoveGroundSlash();
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
        }
    }

    private void MoveGroundSlash()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        destination = ray.GetPoint(1000);
        RotateToDestination(destination, true);
        rb.velocity = transform.forward * speed;
    }

    private void RotateToDestination(Vector3 destination, bool onlyY)
    {
        var direction = destination - transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }
        transform.localRotation = Quaternion.Lerp(transform.rotation, rotation, 1);
    }

    IEnumerator SlowDown()
    {
        while(speed > 0)
        {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, speed);
            speed -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }
        if(speed <= 0)
        {
            isStopped = true;
            Destroy(this.gameObject);
        }
    }
}
