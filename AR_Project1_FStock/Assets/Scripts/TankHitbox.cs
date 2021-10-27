using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHitbox : MonoBehaviour
{

    private int team;
    
    private void OnEnable()
    {
        team = GetComponentInParent<Tank>().GetTeam();
    }

    private void OnCollisionEnter(Collision collision)
    {
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
}
