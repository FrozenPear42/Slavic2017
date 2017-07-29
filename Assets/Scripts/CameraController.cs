using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 playerOffset;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        playerOffset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newOffset = player.transform.position - transform.position;
        Vector3 delta = newOffset - playerOffset;
        transform.position += delta;
    }
}