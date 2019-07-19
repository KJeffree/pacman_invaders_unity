using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    Pacman pacman;

    void Start()
    {
        pacman = FindObjectOfType<Pacman>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionX = collision.GetContact(0).point.x;
        Debug.Log("Wall collision enter at xPos: " + collisionX);
        pacman.EnterWall(collisionX);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        pacman.ExitWall();
    }
}
