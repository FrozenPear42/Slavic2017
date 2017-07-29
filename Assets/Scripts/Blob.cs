﻿using System.Collections;
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

    private float epsilon = 1f;

    private float movingTimeStamp = 0f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 targetPosition2;
    private float interval = 1f;

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
        else 
        if (isMoving)
        {
            if (movingTimeStamp >= interval)
            {
                Debug.Log("blob");
                move(targetPosition, movementSpeed, interval);
                movingTimeStamp = 0f;
            }

            movingTimeStamp += Time.deltaTime;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    protected virtual void setIdle(bool idle)
    {
        isIdle = idle;
    }

    public void addForce(Vector3 position, float speed, float baseForce = 100f)
    {
        Debug.Log("Add force: targetPosition: " + position + " this position: " + transform.position);
        isIdle = false;
        movementSpeed = speed;
        position = Vector3.Normalize(new Vector3(position.x, 0f, position.z));
        blobRigidBody.AddForce(new Vector3(baseForce * position.x, 0, baseForce * position.z) * movementSpeed * Time.deltaTime);
    }

    public void moveToPosition(Vector3 position, float speed, float intervals)
    {
        isMoving = true;
        isIdle = false; 
        interval = intervals;
        targetPosition = position - transform.position;
        targetPosition2 = position;
    }

    private void move(Vector3 position, float speed, float interval)
    {
        if (Vector3.Distance(transform.position, targetPosition2) <= epsilon)
        {
            Debug.Log("stopped");
            blobRigidBody.velocity = Vector3.zero;
            isMoving = false;
            return;
        }

        addForce(position, speed, baseForce);
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
