using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlob : Blob
{

    public GameObject targetSlot;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        setIdle(false);
        moveToPosition(targetSlot.transform.position, movementSpeed*10, 1f);
        GetComponent<AudioSource>().Play();
    }
}
