﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetector : MonoBehaviour
{

    SceneLoader sceneLoader;

    int timesCalled = 0;

    Pacman pacman;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        pacman = FindObjectOfType<Pacman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ghost" && timesCalled == 0)
        {
            pacman.Die();
            sceneLoader.LoadLoseScene();
            timesCalled++;
        }
    }
}
