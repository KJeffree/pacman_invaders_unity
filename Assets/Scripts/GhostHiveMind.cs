using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHiveMind : MonoBehaviour
{
    public List<Ghost> ghosts = new List<Ghost>();
    private static float lastWallHit = 0.0f;
    static float fireDelay = 1;
    static float nextFire = 1;

    public void FirePill()
    {
        if (Time.time > nextFire)
        {
            int randomIndex = Random.Range(0, ghosts.Count);
            ghosts[randomIndex].FirePill();
            nextFire = Time.time + fireDelay;    
        }
    }


    public void HitWall()
    {
        float now = Time.time;
        if (now - lastWallHit > 0.1f) {
            ReverseDirection();
            MoveDown();
            lastWallHit = now;
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void ReverseDirection()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ReverseDirection();
        }
    }

    public void MoveDown()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.MoveDown();
        }
    }

    public void AddGhost(Ghost newGhost)
    {
        Debug.Log("added ghost");
        ghosts.Add(newGhost);
    }

    public void RemoveGhost(Ghost newGhost)
    {
        Debug.Log("ghost removed");
        ghosts.Remove(newGhost);
    }

}
