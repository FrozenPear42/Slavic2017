using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float scanRange = 10.0f;

    public float movementSpeed = 20.0f;
    public float cooldownPeriodInSeconds = 4f;
    public float baseForce = 100.0f;

    private float timeStamp = 0f;

    private Rigidbody blobRigidBody;

    private BlobSpawner blobSpawner;

    private bool isIdle = true;

    private float moveBackForce = 10f;
    private float travelRange = 2f;
    private Vector3 originPosition;

    // Use this for initialization
    protected virtual void Start()
    {
        originPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        blobRigidBody = GetComponent<Rigidbody>();
    }

    public void SetBlobSpawner(BlobSpawner spawner) {
        blobSpawner = spawner;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isIdle)
        {
            OsmoticMove();
        }
    }

    protected virtual void setIdle(bool idle)
    {
        isIdle = idle;
    }

    public void addForce(Vector3 position, float speed)
    {
        isIdle = false;
        movementSpeed = speed;
        blobRigidBody.AddForce(new Vector3(baseForce * position.x, 0, baseForce * position.z) * movementSpeed * Time.deltaTime);
    }

    public Collider[] getCollidersInRange()
    {
        return Physics.OverlapSphere(transform.position, scanRange);
    }

    public void OsmoticMove()
    {
        if (timeStamp >= cooldownPeriodInSeconds)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);

            if (Vector3.Distance(transform.position, originPosition) <= travelRange)
                blobRigidBody.AddForce(new Vector3(baseForce * x, 0, baseForce * y) * movementSpeed * Time.deltaTime);
            else
            {
                Vector3 moveBackVector = (originPosition - transform.position);
                Debug.Log("move back: origin: " + originPosition + " pos: " + transform.position);
                blobRigidBody.AddForce(new Vector3(moveBackVector.x*baseForce, 0, moveBackVector.z * baseForce) * moveBackForce * Time.deltaTime);
          
            }

            timeStamp = 0f;
            Debug.Log("Change rotation");
        }
        timeStamp = timeStamp + Time.deltaTime;
    }

}
