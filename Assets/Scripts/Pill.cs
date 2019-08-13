using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    private Level level;
    private Rigidbody2D pill;

    BonusGhost bonusGhost;

    float speed = 5.00f;

    void Start()
    {
        bonusGhost = FindObjectOfType<BonusGhost>();
        pill = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
    }

    void Update()
    {
        pill.velocity = new Vector2(0, speed);

        if (pill.position.y >= 11.0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ghost")
        {
            collision.gameObject.GetComponent<Ghost>().Die();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            level.UpdateScoreGhost();
        }
        else if (collision.tag == "Bonus Ghost")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            level.UpdateScoreBonusGhost();
        }
        else if (collision.tag == "Barricade")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Barricade>().DegradeBarricade();
        }
    }
    
}
