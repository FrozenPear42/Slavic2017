using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlobFollower : MonoBehaviour, ISoundReactive{

    GameObject toFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(toFollow != null)
            gameObject.transform.position = toFollow.transform.position;
	}

    public void reactOnSound(PlayerMovement player)
    {
        Destroy(gameObject);
    }

    public void setFollow(GameObject toFollow)
    {
        this.toFollow = toFollow;
    }
}
