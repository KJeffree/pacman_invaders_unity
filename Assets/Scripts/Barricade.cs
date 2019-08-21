using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] Sprite[] barricadeStates;
    int numberOfHits = 0;
    // Start is called before the first frame update

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
        } else {
            Destroy(gameObject);
        }
        
    }

    private void ChangeBarricadeState()
    {
        Debug.Log(numberOfHits);
        gameObject.GetComponent<SpriteRenderer>().sprite = barricadeStates[numberOfHits];
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();
    }
}
