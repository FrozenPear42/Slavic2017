using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
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

    private bool isEmmitingSound = false;

    public bool IsEmmitingSound
    {
        get { return isEmmitingSound; }
    }

    private bool hasAnyFollower = false;
    public bool HasAnyFollowers { get; set; }

    private bool hasGreenFollower = false;

    public bool HasGreenFollower
    {
        get { return hasGreenFollower; }
        set
        {
            hasAnyFollower = true;
            hasGreenFollower = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        playerBody.AddForce(new Vector3(baseForce * x, 0, baseForce * y));

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
            lastSoundEventTime = Time.fixedTime;
            isEmmitingSound = true;
            findSoundReactObjects();
        }
    }

    void findSoundReactObjects()
    {
        Collider[] objects = Physics.OverlapSphere(this.playerBody.position, soundRadius);
        foreach (var o in objects)
        {
            ISoundReactive react = o.GetComponent<ISoundReactive>();
            if(react != null)
                react.reactOnSound(this);
        }
    }

}