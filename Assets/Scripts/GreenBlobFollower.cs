using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlobFollower : MonoBehaviour, ISoundReactive{

    GameObject toFollow;

    GreenBlobFollower(GameObject toFollow)
    {
        this.toFollow = toFollow;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = toFollow.transform.position;
	}

    public void reactOnSound(PlayerMovement player)
    {
        Destroy(gameObject);
    }
}
