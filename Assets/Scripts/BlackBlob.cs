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

    Animator animator;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        bool isGreenAbsorbed = false;
        bool isRedAbsorbed = false;
        bool isBlueAbsorbed = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        var blob = collision.gameObject.GetComponent<Blob>();
        if (blob != null && blob.GetType() == typeof(BlueBlob) && !isBlueAbsorbed)
        {
            Destroy(collision.gameObject);
            isBlueAbsorbed = true;
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("Eat");
        }
        if (blob != null && blob.GetType() == typeof(GreenBlob) && !isGreenAbsorbed)
        {
            Destroy(collision.gameObject);
            isGreenAbsorbed = true;
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("Eat");
        }
        if (blob != null && blob.GetType() == typeof(RedBlob) && !isRedAbsorbed)
        {
            Destroy(collision.gameObject);
            isRedAbsorbed = true;
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("Eat");
        }

        if (isGreenAbsorbed && isRedAbsorbed && isBlueAbsorbed)
        {
            GameObject white = Instantiate(whiteBlobPrefab);
            white.transform.position = transform.position;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}