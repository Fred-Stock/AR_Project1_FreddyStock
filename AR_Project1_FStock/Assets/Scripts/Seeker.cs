using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{

    private float followWeight = 3f;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        maxSpeed = .3f;

        if(TeamManager.GetSeeker() != null)
        {
            GameObject tempSeeker = TeamManager.GetSeeker();
            TeamManager.SetSeeker(gameObject);
            Destroy(tempSeeker);
        }
        else{
            TeamManager.SetSeeker(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forceVec = Vector3.zero;

        if(TeamManager.GetRunners().Count != 0)
        {
            forceVec += Follow(FindClosestRunner()) * followWeight;

            forceVec.Normalize();

            rBody.AddForce(forceVec, ForceMode.Force);

        }

        float overSpeed = rBody.velocity.magnitude - maxSpeed;
        if (overSpeed > 0)
        {
            rBody.AddForce(-rBody.velocity.normalized * (overSpeed), ForceMode.VelocityChange);
        }


        transform.LookAt(transform.position + rBody.velocity);
        Debug.DrawLine(transform.position, transform.position + rBody.velocity, Color.black);

        //keeps seeker on the plane
        if (!Physics.Raycast(transform.position, -transform.up))
        {
            rBody.velocity = -rBody.velocity;
        }

    }

    private GameObject FindClosestRunner()
    {
        float minDist = float.MaxValue;

        GameObject clostestRunner = null;
        for(int i = 0; i < TeamManager.GetRunners().Count; i++)
        {
            float sqrDist = (TeamManager.GetRunners()[i].transform.position - transform.position).sqrMagnitude;
            if (sqrDist < minDist)
            {
                minDist = sqrDist;
                clostestRunner = TeamManager.GetRunners()[i];
            }
        }

        return clostestRunner;
    }




}
