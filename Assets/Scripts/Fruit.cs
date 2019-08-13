using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private Rigidbody2D fruit;

    float speed = 2.00f;

    
    void Start()
    {
        fruit = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        fruit.velocity = new Vector2(0, -speed);

        if (fruit.position.y <= -1.0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pacman")
        {
            Destroy(gameObject);
            //    Increase player score??
        }
        
        
    }
}
