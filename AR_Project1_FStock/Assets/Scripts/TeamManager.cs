using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//static class which holds game data
public static class TeamManager
{
    public static int eaten;

    public static List<GameObject> runners = new List<GameObject>();

    private static GameObject seeker;

    public static void RemoveRunner(GameObject runner) { runners.Remove(runner); }

    public static void SetSeeker(GameObject newSeeker) { seeker = newSeeker; }
    public static GameObject GetSeeker() { return seeker; }

    public static List<GameObject> GetRunners() { return runners; }


}
