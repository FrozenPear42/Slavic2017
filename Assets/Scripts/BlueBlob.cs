﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlob : Blob {

    public float player_wage;
    public float run_speed;
    public float eps = 1f;

    bool are_there_bad_guys = false;

    override protected void Start () {
        base.Start();
        base.setIdle(true);
	}

	override protected void Update () {
        base.Update();
        Vector3 avg = GetMeanBadGuysPositon();
        if (are_there_bad_guys) {
            Vector3 diff = transform.position - avg;
            base.addForce(diff, run_speed/diff.magnitude, base.baseForce);
        }
	}

    Vector3 GetMeanBadGuysPositon() {
        Collider[] colliders = base.getCollidersInRange();
        Vector3 average = Vector3.zero;
        Vector3 player_position = Vector3.zero;

        are_there_bad_guys = false;
        int num = 0;
        foreach (Collider col in colliders) {
            if (col.GetComponent<Player>() != null) {
                player_position = col.transform.position;
                are_there_bad_guys = true;
            }
            if (col.GetComponent<GreenBlob>() != null) {
                are_there_bad_guys = true;
                num++;
                average += col.transform.position;
            }  
        }
        if (!are_there_bad_guys) return Vector3.zero;

        if (num > 0)
            average = average / num;
        else
            average = player_position;

        average = (average + player_wage * player_position) / (player_wage+1);
        return average;
    }
}
