using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour, ITouchWalls
{
    float speed = 10.00f;

    [SerializeField] AudioClip pacmanDie;

    [SerializeField] AudioClip pacmanHit;

    public GameObject pill;
    public Transform pillSpawn;
    public float fireRate;
    private float nextFire;
    private Animator animator;

    float animationTimout;
    bool touchingLeftWall = false;
    bool touchingRightWall = false;

    [SerializeField] AudioClip shootPill;

    void Start()
    {
        StartCoroutine(WaitAndLoadStart());
        animator = this.GetComponent<Animator>();
    }

    IEnumerator WaitAndLoadStart()
    {
        yield return new WaitForSeconds(4);
        InvokeRepeating("PacmanBehaviour", 0, 0.02f);
    }

    IEnumerator WaitAndLoadHit()
    {
        yield return new WaitForSeconds(0.5f);
        InvokeRepeating("PacmanBehaviour", 0, 0.02f);
    }

    private void PacmanBehaviour()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (BlockedByWall(deltaX)) deltaX = 0;
        transform.Translate(deltaX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            animator.SetInteger("Action", 1);
            nextFire = Time.time + fireRate;
            Instantiate(pill, pillSpawn.position, pillSpawn.rotation);
            animationTimout = Time.time + 0.15f;
            AudioSource.PlayClipAtPoint(shootPill, Camera.main.transform.position);
        }

        if (Time.time > animationTimout && animator.GetInteger("Action") != 2)
        {
            animator.SetInteger("Action", 0);
        }
    }

    public void Die()
    {
        animator.SetInteger("Action", 2);
        AudioSource.PlayClipAtPoint(pacmanDie, Camera.main.transform.position);
    }

    public void HitByPill()
    {
        CancelInvoke("PacmanBehaviour");
        AudioSource.PlayClipAtPoint(pacmanHit, Camera.main.transform.position);
        Barricade barricade = FindObjectOfType<Barricade>();
        GetComponent<Transform>().position = new Vector3(barricade.transform.position.x, transform.position.y, transform.position.z);
        StartCoroutine(WaitAndLoadHit());

    }

    private bool BlockedByWall(float deltaX)
    {
        var blockedByLeftWall = MovingLeft(deltaX) && touchingLeftWall;
        var blockedByRightWall = MovingRight(deltaX) && touchingRightWall;
        return blockedByLeftWall || blockedByRightWall;
    }

    private bool MovingLeft(float deltaX)
    {
        return deltaX < 0;
    }

    private bool MovingRight(float deltaX)
    {
        return deltaX > 0;
    }

    public void EnterWall(float xPos)
    {
        touchingLeftWall = xPos < SceneDimensions.centreX;
        touchingRightWall = xPos > SceneDimensions.centreX;
    }

    public void ExitWall()
    {
        touchingLeftWall = false;
        touchingRightWall = false;
    }
}
