﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] Sprite[] barricadeStates;
    int numberOfHits = 0;
    public void Restore()
    {
        numberOfHits = 0;
        ChangeBarricadeState();
    }

    public void DegradeBarricade()
    {
        if (numberOfHits < 12){
            numberOfHits++;
            ChangeBarricadeState();
        }
        else if (numberOfHits == 12)
        {
            numberOfHits++;
            gameObject.GetComponent<SpriteRenderer>().sprite = barricadeStates[numberOfHits];
            Destroy(GetComponent<BoxCollider2D>());
        }
        
    }

    private void ChangeBarricadeState()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = barricadeStates[numberOfHits];
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();
    }
}
