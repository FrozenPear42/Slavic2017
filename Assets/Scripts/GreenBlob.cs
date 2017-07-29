using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlob : Blob {
    Player player;

    public GameObject greenBlobFollower;
    public float range; 
    bool followPlayer = false;

    // Use this for initialization
    override protected void Start () {
        player = FindObjectOfType<Player>();
        base.setIdle(true);
    }
	
	// Update is called once per frame
	override protected void Update () {
        if ((player.transform.position - transform.position).magnitude < range)
        {
            player.HasGreenFollower = true;
            GameObject follower = GameObject.Instantiate<GameObject>(greenBlobFollower, player.transform.position, Quaternion.identity);
            follower.GetComponent<GreenBlobFollower>().setFollow(player.gameObject);
            Destroy(gameObject);
        }
	}
}
