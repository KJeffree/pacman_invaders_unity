using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private Rigidbody2D fruit;
    private Level level;
    private Barricade[] barricades;

    private Pacman pacman;
    float speed = 4.00f;

    
    void Start()
    {
        fruit = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        barricades = FindObjectsOfType<Barricade>();
        pacman = FindObjectOfType<Pacman>();
    }

    
    void Update()
    {
        fruit.velocity = new Vector2(0, -speed);

        if (fruit.position.y <= -1.0)
        {
            Destroy(gameObject);
        }
    }

    private void RestoreBarricades()
    {
        foreach (Barricade barricade in barricades)
        {
            barricade.Restore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Pacman")
        {
            switch (gameObject.tag)
            {
                case "Cherry":
                    level.UpdateScoreCherry();
                    break;
                case "Strawberry":
                    RestoreBarricades();
                    break;
                case "Peach":
                    pacman.MakeInvincible();
                    break;
                case "Apple":
                    level.IncreaseLives();
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
            collision.gameObject.GetComponent<Pacman>().EatFruit();
        }
        
        
    }
}
