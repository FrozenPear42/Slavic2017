using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlob : Blob
{
    public GameObject whiteBlobPrefab;
    public GameObject whiteSlot;
    bool isGreenAbsorbed;
    bool isRedAbsorbed;
    bool isBlueAbsorbed;

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
        if (blob != null && blob.GetType() == typeof(BlueBlob) && !isBlueAbsorbed) {
            Destroy(collision.gameObject);
        }
        if (blob != null && blob.GetType() == typeof(GreenBlob) && !isGreenAbsorbed) {
            Destroy(collision.gameObject);
        }
        if (blob != null && blob.GetType() == typeof(RedBlob) && !isRedAbsorbed) {
            Destroy(collision.gameObject);
        }

        if (isGreenAbsorbed && isRedAbsorbed && isBlueAbsorbed)
        {
            var white = Instantiate(whiteBlobPrefab);
            white.transform.position = transform.position;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}