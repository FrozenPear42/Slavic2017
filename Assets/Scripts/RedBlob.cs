using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlob :  Blob, ISoundReactive {
    public GameObject blobPrefab;
	// Use this for initialization
	override protected void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    public void reactOnSound(Player player)
    {
        addChargeForce(player.gameObject.transform.position - transform.position, 20*movementSpeed);
        //base.moveToPosition(player.gameObject.transform.position, base.movementSpeed, 2f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Blob>() != null)
        {
            Destroy(collision.gameObject);
            GameObject blob = Instantiate(blobPrefab, transform.position, Quaternion.identity, transform);
            blob.GetComponent<PurpleBlob>().ForceStart();
            Destroy(gameObject);
        }
        else 
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            // todo: move to starting position

            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        Debug.Log("On destroy red blob!");
    }
}
