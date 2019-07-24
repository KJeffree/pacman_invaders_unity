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
        collision.gameObject.GetComponent<ITouchWalls>().EnterWall(collisionX);

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<ITouchWalls>().ExitWall();
    }
}
