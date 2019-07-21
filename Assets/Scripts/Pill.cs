﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    private Transform pill;
    float speed = 0.30f;

    // Start is called before the first frame update
    void Start()
    {
        pill = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pill.position += Vector3.up * speed;

        if (pill.position.y >= 10)
        {
            Destroy(gameObject);
        }
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
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
        else if (collision.tag == "Pacman")
        {
            //    Pacman lose a life?? 
            Destroy(gameObject);
        }
    }
    */
}
