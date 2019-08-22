using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHiveMind : MonoBehaviour
{
    public List<Ghost> ghosts = new List<Ghost>();
    private static float lastWallHit = 0.0f;
    static float fireDelay = 1f;
    static float nextFire = 1;
    [SerializeField] Fruit[] fruits;

    public void AssignFruit()
    {
        Level level = FindObjectOfType<Level>();
        int i=0;
        int amount = 4;
        while (i < amount)
        {
            int ghostIndex = Random.Range(0, ghosts.Count-1);
            int index = Random.Range(0, Mathf.Clamp(i, 0, level.GetWaveNumber()) + 1);
            ghosts[ghostIndex].SetFruit(fruits[index]);
            i++;
        }
    }

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
        ghosts.Add(newGhost);
    }

    public void RemoveGhost(Ghost newGhost)
    {
        ghosts.Remove(newGhost);
    }

}
