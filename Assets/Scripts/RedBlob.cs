using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlob :  Blob, ISoundReactive { 

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
        //addForce(player.transform.position, movementSpeed);
        base.moveToPosition(player.transform.position, base.movementSpeed, 2f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Blob>() != null || collision.gameObject.GetComponent<Player>() != null)
        {
            Destroy(collision.gameObject);
            Debug.Log("spawn fioletowego");
            Destroy(this);
        }
        
    }

    private void OnDestroy()
    {
        Debug.Log("On destroy red blob!");
    }
}
