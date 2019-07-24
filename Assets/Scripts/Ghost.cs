using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ITouchWalls
{
    bool movingRight = true;
    float speed = 1.0f;
    Level level;

    [SerializeField] GameObject ghostPill;
    [SerializeField] Transform ghostPillSpawn;

    void Start()
    {
        GhostHiveMind.AddGhost(this);
        level = FindObjectOfType<Level>();
        level.CountGhosts();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = speed * Time.deltaTime;
        if (!movingRight)
        {
            deltaX = -deltaX;
        }
        transform.Translate(deltaX, 0, 0);
        GhostHiveMind.FirePill();
    }

    public void FirePill()
    {
        Instantiate(ghostPill, ghostPillSpawn.position, ghostPillSpawn.rotation);
    }
    public void ReverseDirection()
    {
        movingRight = !movingRight;
    }

    public void EnterWall(float xPos)
    {
        Debug.Log("Ghost entered wall");
        GhostHiveMind.HitWall();
    }

    public void ExitWall()
    {
        Debug.Log("Ghost exited wall");
    }

    public void MoveDown()
    {
        transform.Translate(0, -0.3f, 0);
    }

    public void Die()
    {
        GhostHiveMind.RemoveGhost(this);
        level.RemoveGhost();
    }
}
