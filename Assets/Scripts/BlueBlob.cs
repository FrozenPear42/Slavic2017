using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlob : Blob {

    public float player_wage;
    public float run_speed;
    public float eps = 1f;

    bool are_there_bad_guys = false;

    Vector3 where_to_go = Vector3.zero;
    GameObject light;

    override protected void Start () {
        base.Start();
        base.setIdle(true);
        light = GameObject.Find("Point light");
        scanRange = 5;
	}

	override protected void Update () {
        base.Update();
        Vector3 avg = GetMeanBadGuysPositon();
        where_to_go = avg;
        if (are_there_bad_guys) {
            Debug.Log("HE IS HERE. RUN FOR YOUR LIVES");
            where_to_go = transform.position + (transform.position - avg).normalized * (scanRange + 1);
            base.moveToPosition(where_to_go, run_speed);
            light.transform.position = where_to_go;
        }

        Debug.Log((transform.position - where_to_go).magnitude);
        if ((transform.position - where_to_go).magnitude < eps) {
            Debug.Log("IM HERE, RESTING");
            base.setIdle(true);
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
