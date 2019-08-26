using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] Sprite[] barricadeStates;
    int numberOfHits = 0;
<<<<<<< HEAD
=======
    // Start is called before the first frame update

    public void Restore()
    {
        numberOfHits = 0;
        ChangeBarricadeState();
    }
>>>>>>> 891389560d2992204588b33eaf6d5a006bd6d2ae

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
