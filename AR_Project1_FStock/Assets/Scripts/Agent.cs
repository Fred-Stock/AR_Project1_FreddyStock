using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    protected Rigidbody rBody;
    protected BoxCollider bColl;
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected int team;
    protected GameObject goal;

    protected GameObject teamGoal;
    protected List<GameObject> friendlyTanks;
    protected List<GameObject> enemyTanks;

    protected float obstacleDist = .3f;

    protected float obstacleWeight = 1f;
    protected float goalWeight = 3f;

    protected float wanderDist = .3f;
    protected float wanderInterval = .25f;
    protected float wanderTimer;
    protected float wanderWeight = 3f;
    protected Vector3 wanderPoint;

    protected float maxSpeed;

    protected virtual void OnEnable()
    {
        wanderTimer = wanderInterval;//equal to wanderInterval so that the first call of wander will return a vector
        rBody = GetComponent<Rigidbody>();
        bColl = GetComponent<BoxCollider>();
    }

    protected Vector3 Seek(Vector3 target)
    {

        target.y = transform.position.y; //ensure tanks dont start flying

        Vector3 desireDir = target - transform.position;
        //Debug.Log(desireDir);
        desireDir = desireDir.normalized * maxSpeed;
        desireDir -= rBody.velocity;
        return desireDir.normalized;
    }

    protected Vector3 Follow(GameObject targetObj)
    {
        float distToTarget = (targetObj.transform.position - transform.position).magnitude;
        float targetVel = targetObj.GetComponent<Rigidbody>().velocity.magnitude;

        Vector3 tarLead = targetObj.transform.forward * Mathf.Min(distToTarget, targetVel);
        return Seek(targetObj.transform.position + tarLead);
    }


    protected Vector3 Flee(Vector3 scaryTarget)
    {
        return -Seek(scaryTarget);
    }

    protected Vector3 Avoid(GameObject scaryObj)
    {
        return -Follow(scaryObj);
    }


    protected Vector3 Wander()
    {

        if(wanderTimer > wanderInterval)
        {
            float curAngle = Mathf.Atan2(transform.forward.z, transform.forward.x);

            float newAngle = curAngle + Random.Range(Mathf.Deg2Rad * -5, Mathf.Deg2Rad * 5);
            wanderPoint = new Vector3(Mathf.Cos(newAngle), 0, Mathf.Sin(newAngle));
            wanderPoint *= wanderDist;
            wanderTimer -= wanderInterval;
        }
        wanderTimer += Time.deltaTime;

        return Seek(wanderPoint + transform.position);
    }

    public int GetTeam()
    {
        return team;
    }
}
