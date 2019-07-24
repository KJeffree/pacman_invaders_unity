using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHiveMind : MonoBehaviour
{
    private static List<Ghost> ghosts = new List<Ghost>();

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
}
