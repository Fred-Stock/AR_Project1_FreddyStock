using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{

    private float followWeight = 3f;

    private float shootCooldown = 1f;
    private float cooldownTimer = int.MaxValue;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        maxSpeed = .2f;

        if(TeamManager.GetSeeker() != null)
        {
            GameObject tempSeeker = TeamManager.GetSeeker();
            TeamManager.SetSeeker(gameObject);
            Destroy(tempSeeker);
        }
        else{
            TeamManager.SetSeeker(gameObject);
        }

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

        //if (cooldownTimer >= shootCooldown)
        //{
        //    GameObject possibleEnemy = EnemyInSight();
        //    if (possibleEnemy != null)
        //    {
        //        Shoot(possibleEnemy);
        //    }
        //}
        //else
        //{
        //    cooldownTimer += Time.deltaTime;
        //}

        //rBody.AddForce(Seek(teamGoal.transform.position)*goalWeight, ForceMode.Force);
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

        if (!Physics.Raycast(transform.position, -transform.up))//, out hit))
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

    //private GameObject EnemyInSight()
    //{
    //    RaycastHit hit;
    //    //should make it so the barrel raycast doesnt hit its own barrel
    //    Color DebugColor = Color.red;
    //    if (team == 2) { DebugColor = Color.blue; }
    //    Debug.DrawRay(turretBarrel.transform.position, -turretBarrel.transform.up, DebugColor);
    //    if (!Physics.Raycast(new Ray(turretBarrel.transform.position, -turretBarrel.transform.up), out hit))
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        Debug.Log(gameObject.name + " " + hit.collider.gameObject.name);
    //    }
    //    if (enemyTanks.IndexOf(hit.collider.gameObject) > -1)
    //    {
    //        return hit.collider.gameObject;
    //    }

    //    return null;
    //}

    //private void Shoot(GameObject Enemy)
    //{

    //    Vector3 targetLoc = Enemy.transform.position;
    //    targetLoc.y = 0;// bColl.center.y; //all tanks should have the same height

    //    //turret.transform.LookAt(targetLoc); should already be looking if this method is being called
    //    GameObject tempBullet = Instantiate(bullet, turretBarrel.transform.position, new Quaternion());
    //    tempBullet.GetComponent<Bullet>().Shoot(targetLoc, team);

    //    cooldownTimer = 0;
    //}
    


}
