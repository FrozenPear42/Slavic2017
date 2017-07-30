using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlob : Blob
{

    public GameObject targetSlot;
    public float pullingSpeed = 10f;

    GameObject FindWhiteSlot() {
        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots) {
            Debug.Log(slot.gameObject.name);
            if (slot.gameObject.name == "White ") {
                return slot.gameObject;
            }
        }
        return gameObject;
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        setIdle(false);
        GetComponent<AudioSource>().Play();
        targetSlot = FindWhiteSlot();
    }

    protected override void Update()
    {
        if(targetSlot != null)
            transform.position = Vector3.Lerp(transform.position, targetSlot.transform.position, pullingSpeed*Time.deltaTime);        
    }
}
