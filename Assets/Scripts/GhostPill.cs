using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPill : MonoBehaviour
{

    private Transform ghostPill;
    float speed = 0.30f;

    // Start is called before the first frame update
    void Start()
    {
        ghostPill = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ghostPill.position += Vector3.up * -speed;

        if (ghostPill.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
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
    */
    
}
