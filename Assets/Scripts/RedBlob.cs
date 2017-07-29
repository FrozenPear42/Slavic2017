using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlob : Blob, ISoundReactive
{
    public GameObject blobPrefab;
    // Use this for initialization
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public void reactOnSound(Player player)
    {
        addChargeForce(player.gameObject.transform.position - transform.position, 20 * movementSpeed);
        //base.moveToPosition(player.gameObject.transform.position, base.movementSpeed, 2f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null && collision.gameObject.GetComponent<Blob>() != null && collision.gameObject.GetComponent<PurpleBlob>() == null
            && collision.gameObject.GetComponent<RedBlob>() == null && collision.gameObject.GetComponent<BlackBlob>() == null)
        {
            Destroy(collision.gameObject);
            GameObject blob = Instantiate(blobPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            // todo: move to starting position

            Destroy(gameObject);
        }

    }
}
