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

    public GameObject blackBlobPrefab;


    void Start()
    {
        rockBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    public void reactOnSound(PlayerMovement player)
    {
        if (lastHitTime + resetTime <= Time.time)
        {
            lastHitTime = Time.time;
            hitCount = 0;
        }

        hitCount += 1;

        if (hitCount >= targetHitCount)
        {
            spawnBlackBlob();
        }
    }

    private void spawnBlackBlob()
    {
        Instantiate(blackBlobPrefab, transform);
    }
}