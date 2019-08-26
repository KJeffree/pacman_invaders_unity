using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGhost : MonoBehaviour
{
    private Rigidbody2D bonusGhost;
    public float speed;

    [SerializeField] AudioClip ghostDie;

    Wall wall;

    void Start()
    {
        StartCoroutine(WaitAndLoad());        
        bonusGhost = GetComponent<Rigidbody2D>();
        wall = FindObjectOfType<Wall>();

        Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>(), true);
    }
    
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(4);
        InvokeRepeating("BonusGhostMovement", 0, 0.01f);
    }


    private void BonusGhostMovement()
    {
        bonusGhost.velocity = new Vector2(speed, 0);

        if (bonusGhost.position.x >= 14.0 || bonusGhost.position.x <= -2.0)
        {
            Destroy(gameObject);
        }
    }
    
}
