using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    private Rigidbody2D pill;
    float speed = 5.00f;

    void Start()
    {
        pill = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        pill.velocity = new Vector2(0, speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ghost")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            //    Increase player score??
        }
        else if (collision.tag == "Barricade")
        {
            //    Degrade barricade?? 
            Destroy(gameObject);
        }
    }
    
}
