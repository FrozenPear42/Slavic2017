using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float scanRange = 10.0f;

    public float movementSpeed = 20.0f;
    public float cooldownPeriodInSeconds = 4f;
    public float baseForce = 100.0f;

    public Color mainColor;
    public Color rimColor;
    public float rimPower = 2.2f;

    private Material mainMaterial;
    private ParticleSystem auraParticleSystem;
    private ParticleSystem coreParticleSystem;
    private Light light;

    private float timeStamp = 0f;

    private Rigidbody blobRigidBody;

    protected BlobSpawner blobSpawner;

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
        mainMaterial = GetComponentInChildren<Renderer>().material;
        auraParticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
        coreParticleSystem = GetComponentsInChildren<ParticleSystem>()[1];
        light = GetComponentInChildren<Light>();

    
        mainMaterial.SetColor("_InnerColor", Color.black);
        mainMaterial.SetColor("_RimColor", rimColor);
        mainMaterial.SetFloat("_RimPower", rimPower);


        var color = new Color(mainColor.r, mainColor.g, mainColor.b, 1.0f);
        var auraMain = auraParticleSystem.main;
        auraMain.startColor = new ParticleSystem.MinMaxGradient(color);

        var coreMain = coreParticleSystem.main;
        coreMain.startColor = new ParticleSystem.MinMaxGradient(color);
        

        light.color = mainColor;
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
        if (idle)
            isMoving = false;
    }

    public void addForce(Vector3 position, float speed, float baseForce = 100f)
    {
        //Debug.Log("Add force: targetPosition: " + position + " this position: " + transform.position);
        isIdle = false;
        movementSpeed = speed;
        position = Vector3.Normalize(new Vector3(position.x, 0f, position.z));
        blobRigidBody.AddForce(new Vector3(baseForce * position.x, 0, baseForce * position.z) * movementSpeed * Time.deltaTime);
    }

    public void addChargeForce(Vector3 position, float speed, float force = 100f)
    {
        isIdle = false;
        Debug.Log("Add force: targetPosition: " + position + " this position: " + transform.position);

        position = Vector3.Normalize(new Vector3(position.x, 0f, position.z));
        blobRigidBody.AddForce(new Vector3(force * position.x, 0, force * position.z) * speed * Time.deltaTime);
    }

    public void moveToPosition(Vector3 position, float speed, float intervals)
    {
        isMoving = true;
        isIdle = false; 
        interval = intervals;
        targetPosition = position - transform.position;
        targetPosition2 = position;
        targetPosition.y = transform.position.y;
        targetPosition2.y = 0;
    }

    private void move(Vector3 position, float speed, float interval)
    {
        if (Vector3.Distance(transform.position, targetPosition2) <= epsilon)
        {
            //Debug.Log("stopped");
            blobRigidBody.velocity = Vector3.zero;
            isMoving = false;
            isIdle = true;
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
                //Debug.Log("move back: origin: " + originPosition + " pos: " + transform.position);
                //blobRigidBody.AddForce(new Vector3(moveBackVector.x*baseForce, 0, moveBackVector.z * baseForce) * moveBackForce * Time.deltaTime);
                moveToPosition(originPosition, movementSpeed, 1f);
            }

            timeStamp = 0f;
            //Debug.Log("Change rotation");
        }
        timeStamp = timeStamp + Time.deltaTime;
    }

    protected virtual void OnDestroy()
    {
        BlobSpawner spawner = GetComponentInParent<BlobSpawner>();
        if(spawner != null)
            spawner.BlobDied(gameObject);
    }

}
