using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPill : MonoBehaviour
{
    private Rigidbody2D ghostPill;
    float speed = 5.00f;
    Level level;

    void Start()
    {
        ghostPill = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
    }

    void Update()
    {
        ghostPill.velocity = new Vector2(0, -speed);

        if (ghostPill.position.y <= -1.0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pacman")
        {
            level.LoseLife();
            Destroy(gameObject);
            if (level.GetLives() >= 1)
            {
                collision.gameObject.GetComponent<Pacman>().HitByPill();
            }
        }
        else if (collision.tag == "Barricade")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Barricade>().DegradeBarricade();
        }
    }

}
