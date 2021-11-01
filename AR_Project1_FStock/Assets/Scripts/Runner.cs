using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Agent
{

    [SerializeField] private GameObject runnerPrefab;

    [SerializeField] private float scareDist = .2f;
    [SerializeField] private float scareWeight = 3f;

    private Vector3 wanderVec;
    private int iteration = 0;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.rotation = Random.rotation;
        maxSpeed = .2f;
        TeamManager.runners.Add(gameObject);
        wanderVec = Wander();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forceVec = Vector3.zero;

        wanderVec = Wander();

        forceVec += wanderVec*wanderWeight;



        if (NearSeeker())
        {
            forceVec += Avoid(TeamManager.GetSeeker())*scareWeight;
        }

        transform.LookAt(transform.position + rBody.velocity);

        rBody.AddForce(forceVec, ForceMode.Force);

        float overSpeed = rBody.velocity.magnitude - maxSpeed;
        if (overSpeed > 0)
        {
            rBody.AddForce(-rBody.velocity.normalized * (overSpeed), ForceMode.VelocityChange);
        }

        //keeps runner on the plane
        if(!Physics.Raycast(transform.position, Vector3.down))
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
            TeamManager.eaten++;
            if(iteration < 1){
              for(int i = 0; i < 2; i++){
                GameObject temp = Instantiate(runnerPrefab, transform.position, Random.rotation);
                temp.GetComponent<Runner>().SetIteration(iteration+1);
                temp.transform.localScale /= (iteration + 2);
              }
            }

            Destroy(gameObject);

        }
    }

    public void SetIteration(int i){
      this.iteration = i;
    }

}
