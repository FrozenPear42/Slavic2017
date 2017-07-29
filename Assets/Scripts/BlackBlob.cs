using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlob : Blob
{
    public GameObject whiteBlobPrefab;
    public GameObject whiteSlot;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        var blob = collision.gameObject.GetComponent<Blob>();
        if (blob != null && blob.GetType() == typeof(BlueBlob))
        {
            var white = Instantiate(whiteBlobPrefab);
            white.transform.position = transform.position;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}