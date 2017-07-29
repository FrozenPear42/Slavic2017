using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlob : Blob
{

    public GameObject targetSlot;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        moveToPosition(targetSlot.transform.position, movementSpeed, 0.5f);
	}

    // Update is called once per frame
    override protected void Update () {
		
	}
}
