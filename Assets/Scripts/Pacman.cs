using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour, ITouchWalls
{
    float speed = 10.00f;

    [SerializeField] AudioClip pacmanDie;
    [SerializeField] AudioClip pacmanHit;
    [SerializeField] AudioClip pacmanEatFruit;
    [SerializeField] AudioClip shootPill;

    public GameObject pill;
    public Transform pillSpawn;
    public float fireRate;
    private float nextFire;
    private Animator animator;
    bool pacmanInvincible = false;

    GhostHiveMind ghostHiveMind;

    float animationTimout;
    bool touchingLeftWall = false;
    bool touchingRightWall = false;

    bool colour = true;

    void Start()
    {
        StartCoroutine(WaitAndLoadStart());
        animator = this.GetComponent<Animator>();
        ghostHiveMind = FindObjectOfType<GhostHiveMind>();

    }

    public void PreventShooting()
    {
        CancelInvoke("PacmanBehaviour");
        InvokeRepeating("PacmanMovement", 0, 0.02f);
        StartCoroutine(WaitAndLoadStart());
    }


    IEnumerator WaitAndLoadStart()
    {
        yield return new WaitForSeconds(4);
        CancelInvoke("PacmanMovement");
        InvokeRepeating("PacmanBehaviour", 0, 0.02f);
    }

    IEnumerator WaitAndLoadHit()
    {
        yield return new WaitForSeconds(0.5f);
        InvokeRepeating("PacmanBehaviour", 0, 0.02f);
    }

    private void PacmanBehaviour()
    {
        PacmanMovement();

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

    private void PacmanMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (BlockedByWall(deltaX)) deltaX = 0;
        transform.Translate(deltaX, 0, 0);
    }

    public void Die()
    {
        CancelInvoke("PacmanBehaviour");
        Destroy(GetComponent<PolygonCollider2D>());
        animator.SetInteger("Action", 2);
        AudioSource.PlayClipAtPoint(pacmanDie, Camera.main.transform.position);
        ghostHiveMind.StopGhostMovement();
    }

    public void HitByPill()
    {
        CancelInvoke("PacmanBehaviour");
        AudioSource.PlayClipAtPoint(pacmanHit, Camera.main.transform.position);
        Barricade barricade = FindObjectOfType<Barricade>();
        if (barricade)
        {
            GetComponent<Transform>().position = new Vector3(barricade.transform.position.x, transform.position.y, transform.position.z);
            StartCoroutine(WaitAndLoadHit());
        }
        else
        {
            GetComponent<Transform>().position = new Vector3(1.60f, transform.position.y, transform.position.z);
            StartCoroutine(WaitAndLoadHit());
        }
    }

    public bool GetPacmanInvincibility()
    {
        return pacmanInvincible;
    }

    public void MakeInvincible()
    {
        CancelInvoke("ChangePacmanColour");
        pacmanInvincible = true;
        InvokeRepeating("ChangePacmanColour", 0, 0.2f);
        StartCoroutine(CancelInvincible());
    }

    IEnumerator CancelInvincible()
    {
        yield return new WaitForSeconds(5);
        CancelInvoke("ChangePacmanColour");
        pacmanInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void ChangePacmanColour()
    {
        var color1 = new Color(1, 0, 0, 1);
        var color2 = new Color(1, 1, 1, 1);

        colour = !colour;

        if (colour)
        {
            GetComponent<SpriteRenderer>().color = color1;
        } else 
        {
            GetComponent<SpriteRenderer>().color = color2;
        }
    }

    public void EatFruit()
    {
        AudioSource.PlayClipAtPoint(pacmanEatFruit, Camera.main.transform.position);
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
