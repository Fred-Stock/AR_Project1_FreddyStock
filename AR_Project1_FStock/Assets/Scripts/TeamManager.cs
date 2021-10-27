using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TeamManager
{
    public static List<GameObject> team1Tanks = new List<GameObject>();
    public static List<GameObject> team2Tanks = new List<GameObject>();
    public static List<GameObject> runners = new List<GameObject>();

    private static GameObject team1Goal, team2Goal;

    private static GameObject seeker;

    public static void RemoveTank(GameObject tank)
    {
        //since remove does not through an error if the element does not exist
        //I am calling remove on both lists in lieu of figuring out which list the tank is in
        team1Tanks.Remove(tank);
        team2Tanks.Remove(tank);
    }

    public static void SetTeam1Goal(GameObject goal1) { team1Goal = goal1; }
    public static void SetTeam2Goal(GameObject goal2) { team2Goal = goal2; }

    public static void SetSeeker(GameObject newSeeker) { seeker = newSeeker; }
    public static GameObject GetSeeker() { return seeker; }

    public static GameObject getTeam1Goal() { return team1Goal; }
    public static GameObject getTeam2Goal() { return team2Goal; }


}
