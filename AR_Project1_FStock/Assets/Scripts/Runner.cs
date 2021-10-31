using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Agent
{

    [SerializeField]private float scareDist = .2f;
    [SerializeField] private float scareWeight = 3f;

    private Vector3 wanderVec;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.rotation = Random.rotation;

        maxSpeed = .1f;
        TeamManager.runners.Add(gameObject);
        wanderVec = Wander();
        //if (team == 1)
        //{
        //    TeamManager.team1Tanks.Add(gameObject);
        //    friendlyTanks = TeamManager.team1Tanks;
        //    enemyTanks = TeamManager.team2Tanks;
        //    teamGoal = TeamManager.getTeam1Goal();
        //}
        //else
        //{
        //    TeamManager.team2Tanks.Add(gameObject);
        //    friendlyTanks = TeamManager.team2Tanks;
        //    enemyTanks = TeamManager.team1Tanks;
        //    teamGoal = TeamManager.getTeam2Goal();
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forceVec = Vector3.zero;
        //rBody.AddForce(Seek(teamGoal.transform.position)*goalWeight, ForceMode.Force);

        wanderVec = Wander();

        forceVec += wanderVec*wanderWeight;



        if (NearSeeker())
        {
            forceVec += Avoid(TeamManager.GetSeeker())*scareWeight;
        }

        transform.LookAt(transform.position + rBody.velocity);
        //Debug.DrawLine(transform.position, transform.position + rBody.velocity, Color.black);
        //Debug.DrawLine(transform.position, wanderVec + transform.position, Color.blue);
        //Debug.DrawLine(transform.position, wanderVec, Color.green);
        rBody.AddForce(forceVec, ForceMode.Force);

        float overSpeed = rBody.velocity.magnitude - maxSpeed;
        if (overSpeed > 0)
        {
            rBody.AddForce(-rBody.velocity.normalized * (overSpeed), ForceMode.VelocityChange);
        }

        if(!Physics.Raycast(transform.position, Vector3.down))//, out hit))
        {
            rBody.velocity = -rBody.velocity;
        }
        
    }

    private bool NearSeeker()
    {
        if(TeamManager.GetSeeker() == null) { return false; }
        if ((TeamManager.GetSeeker().transform.position - transform.position).sqrMagnitude < scareDist * scareDist)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Seeker>() != null)
        {
            TeamManager.RemoveRunner(gameObject);
            Destroy(gameObject);
        }
    }

}
