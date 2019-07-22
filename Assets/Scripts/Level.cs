using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{

    SceneLoader sceneLoader;

    [SerializeField] int lives = 3;

    [SerializeField] TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivesText();
    }

    public void LoseLife()
    {
        lives -= 1;
        if (lives <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
