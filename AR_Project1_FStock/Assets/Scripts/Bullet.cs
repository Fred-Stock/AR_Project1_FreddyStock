using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]private int team;
    private Rigidbody rBody;
    [SerializeField] private float speed;


    void OnEnable()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 targetDir, int team)
    {
        this.team = team;
        rBody.AddForce(targetDir.normalized * speed, ForceMode.Impulse);
    }

    public int getTeam() { return team; }
}
