using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlob : Blob {
    PlayerMovement player;
    public float range; 
    bool followPlayer = false;

	// Use this for initialization
	protected void override Start () {
        player = FindObjectOfType<PlayerMovement>();
        base.isIdle = true;
    }
	
	// Update is called once per frame
	protected void override Update () {
        if ((player.transform.position - transform.position).magnitude < range)
        {
            player.HasGreenFollower = true;
            //Spawn folower for player
            Destroy(gameObject);
        }
	}
}
