using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlob : Blob
{

    public Slot targetSlot;

	// Use this for initialization
	void Start () {
        moveToPosition(targetSlot.transform.position, movementSpeed, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
