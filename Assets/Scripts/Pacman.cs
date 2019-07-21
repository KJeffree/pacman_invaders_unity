using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    float speed = 10.00f;


    public GameObject pill;
    public Transform pillSpawn;
    public float shootRate;
    private float nextShoot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed;
        deltaX *= Time.deltaTime;
        transform.Translate(deltaX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShoot )
        {
            nextShoot = Time.time + shootRate;
            Instantiate(pill, pillSpawn.position, pillSpawn.rotation);
        }
    }


}
