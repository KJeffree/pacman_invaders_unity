using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHiveMind : MonoBehaviour
{
    private static List<Ghost> ghosts = new List<Ghost>();
    private static float lastWallHit = 0.0f;
    static float fireDelay = 1;
    static float nextFire = 1;

    public static void FirePill()
    {
        if (Time.time > nextFire)
        {
            int randomIndex = Random.Range(0, ghosts.Count);
            ghosts[randomIndex].FirePill();
            nextFire = Time.time + fireDelay;    
        }
    }


    public static void HitWall()
    {
        float now = Time.time;
        if (now - lastWallHit > 0.1f) {
            ReverseDirection();
            MoveDown();
            lastWallHit = now;
        }
    }

    public static void ReverseDirection()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ReverseDirection();
        }
    }

    public static void MoveDown()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.MoveDown();
        }
    }

    public static void AddGhost(Ghost newGhost)
    {
        ghosts.Add(newGhost);
    }

    public static void RemoveGhost(Ghost newGhost)
    {
        ghosts.Remove(newGhost);
    }

}
