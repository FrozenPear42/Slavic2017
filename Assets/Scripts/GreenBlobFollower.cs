using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlobFollower : MonoBehaviour, ISoundReactive{

    GameObject toFollow = null;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (toFollow != null) { 
            gameObject.transform.position = toFollow.transform.position + new Vector3(1, 0, 1);
        }
    }

    public void reactOnSound(PlayerMovement player)
    {
        Destroy(gameObject);
    }

    public void setFollow(GameObject objToFollow)
    {
        toFollow = objToFollow;
    }
}
