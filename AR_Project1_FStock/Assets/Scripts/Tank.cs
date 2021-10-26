using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Rigidbody rBody;
    private BoxCollider bColl;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretBarrel;
    [SerializeField] private float moveSpeed;

    [SerializeField] private int team;
    private GameObject teamGoal;
    private List<GameObject> friendlyTanks; //make sure its passed as refrence not value
    private List<GameObject> enemyTanks;

    private float shootCooldown = 1f;
    private float cooldownTimer = int.MaxValue;

    // Start is called before the first frame update
    void OnEnable()
    {
        rBody = GetComponent<Rigidbody>();
        bColl = GetComponent<BoxCollider>();

        if(team == 1)
        {
            TeamManager.team1Tanks.Add(gameObject);
            friendlyTanks = TeamManager.team1Tanks;
            enemyTanks = TeamManager.team2Tanks;
        }
        else
        {
            TeamManager.team2Tanks.Add(gameObject);
            friendlyTanks = TeamManager.team2Tanks;
            enemyTanks = TeamManager.team1Tanks;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cooldownTimer >= shootCooldown)
        {
            GameObject possibleEnemy = EnemyInSight();
            if (possibleEnemy != null)
            {
                Shoot(possibleEnemy);
            }
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }
        
    }

    private GameObject EnemyInSight() {
        RaycastHit hit;
        //should make it so the barrel raycast doesnt hit its own barrel
        Color DebugColor = Color.red;
        if(team == 2) { DebugColor = Color.blue; }
        Debug.DrawRay(turretBarrel.transform.position, -turretBarrel.transform.up, DebugColor);
        if (!Physics.Raycast(new Ray(turretBarrel.transform.position, -turretBarrel.transform.up), out hit))
        {
            return null;
        }
        else
        {
            Debug.Log(gameObject.name + " " + hit.collider.gameObject.name);
        }
        if( enemyTanks.IndexOf(hit.collider.gameObject) > -1)
        {
            return hit.collider.gameObject;
        }
       
        return null;
    }

    private void Shoot(GameObject Enemy) {

        Vector3 targetLoc = Enemy.transform.position;
        targetLoc.y = 0;// bColl.center.y; //all tanks should have the same height

        //turret.transform.LookAt(targetLoc); should already be looking if this method is being called
        GameObject tempBullet = Instantiate(bullet, turretBarrel.transform.position, new Quaternion());
        tempBullet.GetComponent<Bullet>().Shoot(targetLoc, team);

        cooldownTimer = 0;
    }

    private Vector3 Seek(Vector3 target) {

        target.y = transform.position.y; //ensure tanks dont start flying

        Vector3 desireDir = target - transform.position;
        desireDir -= rBody.velocity;
        return desireDir;
    }

    private Vector3 Flee(Vector3 scaryTarget) {

        return Seek(scaryTarget);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //check for bullet collision
        //Destroy if enemy
        if (collision.gameObject.CompareTag("bullet"))
        {

            if (collision.gameObject.GetComponent<Bullet>().getTeam() != team)
            {
                TeamManager.RemoveTank(gameObject);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }

    private void AvoidOtherTanks() { 
        //make sure tanks dont run into each other
    }
}
