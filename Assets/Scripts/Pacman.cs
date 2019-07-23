using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    float speed = 10.00f;

    public GameObject pill;
    public Transform pillSpawn;
    public float fireRate;
    private float nextFire;


    void Start()
    {
        
    }

    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed;
        deltaX *= Time.deltaTime;
        transform.Translate(deltaX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(pill, pillSpawn.position, pillSpawn.rotation);
        }
    }


}
