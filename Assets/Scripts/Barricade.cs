using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] Sprite[] barricadeStates;
    int numberOfHits = 0;

    public void DegradeBarricade()
    {
        if (numberOfHits < 12){
            gameObject.GetComponent<SpriteRenderer>().sprite = barricadeStates[numberOfHits];
            Destroy(GetComponent<BoxCollider2D>());
            gameObject.AddComponent<BoxCollider2D>();
            numberOfHits++;
        } else {
            Destroy(gameObject);
        }
        
    }
}
