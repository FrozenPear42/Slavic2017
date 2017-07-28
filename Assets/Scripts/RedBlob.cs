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

    public void reactOnSound(PlayerMovement player)
    {
        moveToPosition(player.transform.position, movementSpeed);
    }


}
