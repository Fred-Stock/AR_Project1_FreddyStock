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

        //rBody.AddForce(Seek(teamGoal.transform.position)*goalWeight, ForceMode.Force);
        if(wanderTimer > wanderInterval)
        {
            wanderTimer -= wanderInterval;
            wanderVec = Wander();
        }
        rBody.AddForce(Wander() * wanderWeight, ForceMode.Force);
        if (NearSeeker())
        {
            rBody.AddForce(Avoid(TeamManager.GetSeeker()) * scareWeight);
        }
        transform.LookAt(transform.position + rBody.velocity);
        Debug.DrawLine(transform.position, transform.position + rBody.velocity, Color.black);


    }

    private bool NearSeeker()
    {
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
