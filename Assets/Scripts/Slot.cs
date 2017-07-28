using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    bool isFilled;
    Blob component;

	void Start () {
		
	}

	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        
        if (false) {
            PullBlob(other.gameObject);
        }
    }

    bool IsFilled() {
        return isFilled;
    }

    void PullBlob(GameObject blob) {
        blob.GetComponent<Blob>().enabled = false;
    }
}
