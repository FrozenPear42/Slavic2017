using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    private Rigidbody playerBody;
    private AudioSource playerAudioSource;

    public string soundKey = "f";
    public string soundButton = "joystick button 2";

    public float baseForce = 100.0f;
    public float soundDelay = 2.0f;
    public float soundRadius = 100.0f;

    public float maxSpeed = 10.0f;

    public GameObject ringPrefab;

    private Animator animator;

    private bool isEmmitingSound = false;

    private Vector3 basePosition;

    public bool IsEmmitingSound
    {
        get { return isEmmitingSound; }
    }

    private bool hasAnyFollower = false;

    private int greenFollowersCount = 0;
    private int anyFollowersCount = 0;
    private bool hasGreenFollower = false;

    public bool HasGreenFollower
    {
        get { return hasGreenFollower; }
        set
        {
            if (value == true)
            {
                ++greenFollowersCount;
                ++anyFollowersCount;
            }
            else
            {
                --greenFollowersCount;
                --anyFollowersCount;
            }
        }
    }
    public bool HasAnyFollower
    {
        get { return hasAnyFollower;  }
        set
        {
            if (value == true)
                ++anyFollowersCount;
            else
                --anyFollowersCount;
        }
    }

    private float lastSoundEventTime;

    // Use this for initialization
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        hasAnyFollower = false;
        hasGreenFollower = false;
        playerAudioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();

        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        playerBody.AddForce(new Vector3(baseForce * x * Time.deltaTime, 0, baseForce * y * Time.deltaTime));
        animator.SetFloat("Speed", playerBody.velocity.magnitude);

        if (isEmmitingSound)
        {
            if (lastSoundEventTime + soundDelay <= Time.fixedTime)
            {
                isEmmitingSound = false;
            }
        }
        if (!isEmmitingSound && (Input.GetKey(soundButton) || Input.GetKey(soundKey)))
        {
            playerAudioSource.Play();
            var ring = Instantiate(ringPrefab, this.gameObject.transform);

            lastSoundEventTime = Time.fixedTime;
            isEmmitingSound = true;
            findSoundReactObjects();
        }

        hasGreenFollower = (greenFollowersCount > 0);
        hasAnyFollower = (anyFollowersCount > 0);

        if (playerBody.velocity.magnitude > maxSpeed)
            playerBody.velocity = maxSpeed * playerBody.velocity.normalized;
    }

    void findSoundReactObjects()
    {
        Collider[] objects = Physics.OverlapSphere(this.playerBody.position, soundRadius);
        foreach (var o in objects)
        {
            ISoundReactive react = o.GetComponent<ISoundReactive>();
            if (react != null)
                react.reactOnSound(this);
        }
    }
}