using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBlob : Blob, ISoundReactive {

    Player player;

    public GameObject greenBlobFollower;
    public float range;
    public float cooldown;
    public float speed;

    enum State { Idle, Following, Cooldown };
    State state = State.Idle;
    float cooldownStart = 0.0f;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        base.setIdle(true);
    }

    public void ForceStart()
    {
        Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Idle:
                if (PlayerInRange())
                    StartFollowing();
                break;
            case State.Following:
                if (!PlayerInRange())
                    Follow();
                break;
            case State.Cooldown:
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
        player.HasAnyFollower = true;
        state = State.Following;
        GetComponent<AudioSource>().Play();
    }

    void Follow()
    {
        Vector3 diff = player.transform.position - transform.position;
        addForce(diff, speed);
    }

    public void reactOnSound(Player player)
    {
        if (state == State.Following)
        {
            state = State.Cooldown;
            cooldownStart = Time.time;
            player.HasAnyFollower = false;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (state == State.Following)
            player.HasAnyFollower = false;
    }
}
