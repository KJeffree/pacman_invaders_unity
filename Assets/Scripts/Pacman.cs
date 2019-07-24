using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour, ITouchWalls
{
    float speed = 10.00f;
    bool touchingLeftWall = false;
    bool touchingRightWall = false;

    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (BlockedByWall(deltaX)) return;
        transform.Translate(deltaX, 0, 0);
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
