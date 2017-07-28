using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlob : Blob {
    PlayerMovement player;
    public float range; 
    bool followPlayer = false;

    // Use this for initialization
    override protected void Start () {
        player = FindObjectOfType<PlayerMovement>();
        base.setIdle(true);
    }
	
	// Update is called once per frame
	override protected void Update () {
        if ((player.transform.position - transform.position).magnitude < range)
        {
            player.HasGreenFollower = true;
            //Spawn folower for player
            Destroy(gameObject);
        }
	}
}
