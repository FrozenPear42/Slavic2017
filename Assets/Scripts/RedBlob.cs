using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlob :  Blob, ISoundReactive { 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void reactOnSound(Player player)
    {
        moveToPosition(player.transform.position, movementSpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Debug.Log("spawn fioletowego");
        Destroy(this);
    }

    private void OnDestroy()
    {
        Debug.Log("On destroy red blob!");
    }
}
