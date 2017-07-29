using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour {
    int successes = 0;

    void Start () {
		
	}

	void Update () {
		
	}

    public void ReportSuccess(Slot slot) {
        successes++;
        if(successes == 5)
        {
            //Debug.Log("You Won");
        }
    }
}
