using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TeamManager
{
    public static List<GameObject> team1Tanks = new List<GameObject>();
    public static List<GameObject> team2Tanks = new List<GameObject>();

    public static void RemoveTank(GameObject tank)
    {
        //since remove does not through an error if the element does not exist
        //I am calling remove on both lists in lieu of figuring out which list the tank is in
        team1Tanks.Remove(tank);
        team2Tanks.Remove(tank);
    }

}
