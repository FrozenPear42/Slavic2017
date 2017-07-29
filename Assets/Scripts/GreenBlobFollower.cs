using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlobFollower : Blob, ISoundReactive{

    GameObject toFollow = null;

    // Use this for initialization
    override protected void Start () {
        
	}

    // Update is called once per frame
    override protected void Update()
    {
        if (toFollow != null) { 
            gameObject.transform.position = toFollow.transform.position + new Vector3(1, 0, 1);
        }
    }

    public void reactOnSound(Player player)
    {
        Destroy(gameObject);
    }

    public void setFollow(GameObject objToFollow)
    {
        toFollow = objToFollow;
    }
}
