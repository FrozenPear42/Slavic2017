using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlob : Blob, ISoundReactive
{
    Player player;

    public GameObject greenBlobFollower;
    public float range;
    public float cooldown;
    public float speed;

    enum State { Idle, Following, Cooldown };
    State state = State.Idle;
    float cooldownStart = 0.0f;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        player = FindObjectOfType<Player>();
        base.setIdle(true);
    }
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
        switch (state) {
            case State.Idle:
                //Debug.Log("Idle");
                if (PlayerInRange())
                    StartFollowing();
                break;
            case State.Following:
                //Debug.Log("Following");
                if (!PlayerInRange())
                    Follow();
                break;
            case State.Cooldown:
                //Debug.Log("Cooldown");
                if ((cooldownStart + cooldown) < Time.time)
                {
                    state = State.Idle;
                    setIdle(true);
                }
                break;
        }
	}

    bool PlayerInRange()
    {
        return (player.transform.position - transform.position).magnitude < range;
    }

    void StartFollowing()
    {
        player.HasGreenFollower = true;
        state = State.Following;
        setIdle(false);
    }

    void Follow()
    {
        Vector3 diff = player.transform.position - transform.position;
        addForce(diff, speed);
    }

    public void reactOnSound(Player player)
    {
        state = State.Cooldown;
        cooldownStart = Time.time;
    }
}
