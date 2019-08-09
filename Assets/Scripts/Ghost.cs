using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ITouchWalls
{
    bool movingRight = true;
    [SerializeField] float speed = 0.5f;
    Level level;

    [SerializeField] GameObject ghostPill;
    [SerializeField] Transform ghostPillSpawn;

    GhostHiveMind ghostHiveMind;

    void Start()
    {
        StartCoroutine(WaitAndLoad());
        ghostHiveMind = FindObjectOfType<GhostHiveMind>();
        ghostHiveMind.AddGhost(this);
        level = FindObjectOfType<Level>();
        level.CountGhosts();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(4);
        InvokeRepeating("ghostMovement", 0, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ghostMovement()
    {
        float deltaX = speed * Time.deltaTime;
        if (!movingRight)
        {
            deltaX = -deltaX;
        }
        transform.Translate(deltaX, 0, 0);
        ghostHiveMind.FirePill();
    }

    public void IncreaseSpeed()
    {
        speed += 0.1f;
    }

    public void FirePill()
    {
        Instantiate(ghostPill, ghostPillSpawn.position, ghostPillSpawn.rotation);
    }
    public void ReverseDirection()
    {
        movingRight = !movingRight;
        speed += 0.01f;
    }

    public void EnterWall(float xPos)
    {
        ghostHiveMind.HitWall();
    }

    public void ExitWall()
    {
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void MoveDown()
    {
        transform.Translate(0, -0.3f, 0);
    }

    public void Die()
    {
        ghostHiveMind.RemoveGhost(this);
        level.RemoveGhost();
    }
}
