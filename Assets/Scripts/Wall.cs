using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionX = collision.GetContact(0).point.x;
        Debug.Log("Wall collision enter at xPos: " + collisionX);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }
}
