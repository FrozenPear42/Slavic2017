using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float scanRange = 10.0f;

    public float movementSpeed = 20.0f;
    public float cooldownPeriodInSeconds = 4f;

    private float timeStamp = 0f;

    private Rigidbody blobRigidBody;
    private Vector3 randomDirectionVector = Vector3.forward;
    private Vector3 newPosition = Vector3.forward;

    // Use this for initialization
    void Start()
    {
        blobRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OsmoticMove();
    }

    Collider[] getCollidersInRange()
    {
        return Physics.OverlapSphere(transform.position, scanRange);
    }

    void OsmoticMove()
    {
        if (timeStamp >= cooldownPeriodInSeconds)
        {
            randomDirectionVector = Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)));
            newPosition = (transform.position + randomDirectionVector) * 0.7f;

            // transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
            timeStamp = 0f;
            Debug.Log("Change rotation");
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, timeStamp / cooldownPeriodInSeconds * Time.deltaTime);
        //blobRigidBody.AddForce(transform.forward * movementSpeed * Time.deltaTime);
        timeStamp = timeStamp + Time.deltaTime;
    }

}
