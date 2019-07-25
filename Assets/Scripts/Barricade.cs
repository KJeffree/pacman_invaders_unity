using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] Sprite[] barricadeStates;
    int numberOfHits = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DegradeBarricade()
    {
        if (numberOfHits < 6){
            gameObject.GetComponent<SpriteRenderer>().sprite = barricadeStates[numberOfHits];
            Destroy(GetComponent<BoxCollider2D>());
            gameObject.AddComponent<BoxCollider2D>();
            numberOfHits++;
        } else {
            Destroy(gameObject);
        }
        
    }
}
