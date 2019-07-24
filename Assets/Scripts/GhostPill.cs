using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPill : MonoBehaviour
{
    private Rigidbody2D ghostPill;
    float speed = 5.00f;

    void Start()
    {
        ghostPill = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ghostPill.velocity = new Vector2(0, -speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pacman")
        {
            //    Pacman lose a life/cannon??
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        }
        else if (collision.tag == "Barricade")
        {
            //    Degrade barricade?? 
            Destroy(gameObject);
        }
    }

}
