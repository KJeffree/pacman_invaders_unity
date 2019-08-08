using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGhost : MonoBehaviour
{
    private Rigidbody2D bonusGhost;
    public float speed;

    Wall wall;

    void Start()
    {
        bonusGhost = GetComponent<Rigidbody2D>();
        wall = FindObjectOfType<Wall>();

        Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>(), true);
    }


    void Update()
    {
        bonusGhost.velocity = new Vector2(speed, 0);

        if (bonusGhost.position.x >= 14.0 || bonusGhost.position.x <= -2.0)
        {
            Destroy(gameObject);
        }
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Not working with current camera aspect
    /* private void OnBecameInvisible()
    {
        Destroy(gameObject);
    } */

    
}
