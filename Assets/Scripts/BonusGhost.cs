using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGhost : MonoBehaviour
{
    private Rigidbody2D bonusGhost;
    float speed = 5.00f;

    void Start()
    {
        bonusGhost = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        bonusGhost.velocity = new Vector2(speed, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
