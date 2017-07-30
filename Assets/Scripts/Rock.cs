using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Api;
using UnityEngine;

public class Rock : MonoBehaviour, ISoundReactive
{
    private Rigidbody rockBody;

    private int hitCount = 0;
    private float lastHitTime;

    public int targetHitCount = 3;
    public float resetTime = 5.0f;
    public float setKinematicAfterTime = 3f;

    public GameObject blackBlobPrefab;
    public GameObject brokenRockPrefab;

    void Start()
    {
        rockBody = GetComponent<Rigidbody>();
        Invoke("SetKinematic", setKinematicAfterTime);
    }

    public float maxSpeed = 1f;
    public float maxAngularSpeed = 0.2f;

    public AudioClip hitSound;
    public AudioClip crashSound;


    int collision_counter = 0;
    bool collided = false;

    void Update()
    {
        /*Rigidbody rigid = GetComponent<Rigidbody>();
        if (rigid.velocity.magnitude > maxSpeed) {
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);
        }
        if (rigid.angularVelocity.magnitude > maxSpeed) {
            rigid.velocity = Vector3.ClampMagnitude(rigid.angularVelocity, maxAngularSpeed);
        }*/
        /*if (collision_counter == 0 && collided) {
            SetKinematic();
        }*/
    }

    /*private void OnCollisionEnter(Collision collision) {
        collided = true;
        collision_counter++;
    }

    private void OnCollisionExit(Collision collision) {
        collision_counter--;
    }*/

    public void reactOnSound(Player player)
    {
        if (lastHitTime + resetTime <= Time.time)
        {
            hitCount = 0;
        }

        hitCount += 1;
        lastHitTime = Time.time;

        if (hitCount >= targetHitCount)
        {
 
            spawnBlackBlob();
        }
        else
        {
            GetComponent<AudioSource>().clip = hitSound;
            GetComponent<AudioSource>().Play();
        }
    }

    private void spawnBlackBlob()
    {
        var black = Instantiate(blackBlobPrefab);
        black.transform.position = transform.position + new Vector3(1f, 0, 0);
        var broken = Instantiate(brokenRockPrefab);
        broken.transform.position = transform.position;
        Destroy(gameObject);
    }

    void SetKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

}