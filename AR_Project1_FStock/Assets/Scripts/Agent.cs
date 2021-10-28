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
    protected List<GameObject> friendlyTanks; //make sure its passed as refrence not value
    protected List<GameObject> enemyTanks;

    protected float obstacleDist = .3f;

    protected float obstacleWeight = 1f;
    protected float goalWeight = 3f;

    protected float wanderDist = .3f;
    protected float wanderInterval = 5f;
    protected float wanderTimer = 0f;
    protected float wanderWeight = 3f;

    protected virtual void OnEnable()
    {
        rBody = GetComponent<Rigidbody>();
        bColl = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Vector3 Seek(Vector3 target)
    {

        target.y = transform.position.y; //ensure tanks dont start flying

        Vector3 desireDir = target - transform.position;
        Debug.Log(desireDir);
        desireDir -= rBody.velocity;
        return desireDir.normalized;
    }

    protected Vector3 Follow(GameObject targetObj)
    {
        Vector3 tarLead = targetObj.transform.forward * targetObj.GetComponent<Rigidbody>().velocity.magnitude;
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
        float curAngle = Mathf.Atan2(rBody.velocity.z, rBody.velocity.x);

        float newAngle = curAngle + Random.Range(Mathf.Deg2Rad * -45, Mathf.Deg2Rad * 45);
        Vector3 wanderPoint = new Vector3(Mathf.Cos(newAngle), transform.position.y, Mathf.Sin(newAngle));
        wanderPoint *= wanderDist;
        wanderPoint += transform.position;

        return Seek(wanderPoint);
    }

    public int GetTeam()
    {
        return team;
    }
}
