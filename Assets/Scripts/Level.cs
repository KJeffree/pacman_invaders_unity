using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{

    [SerializeField] int lives = 3;

    [SerializeField] TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivesText();
    }

    public void LoseLife()
    {
        lives -= 1;
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
