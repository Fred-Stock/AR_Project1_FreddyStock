using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamGoal : MonoBehaviour
{
    [SerializeField]private int team;

    private void OnEnable()
    {
        if(team == 1)
        {
            TeamManager.SetTeam1Goal(gameObject);
        }
        else
        {
            TeamManager.SetTeam2Goal(gameObject);
        }
    }

}
