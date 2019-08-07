using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{

    SceneLoader sceneLoader;

    [SerializeField] int numberOfGhosts = 0;

    [SerializeField] int lives = 3;

    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI livesText;

    public BonusGhost bonusGhost;
    float spawnTime = 15;
    Vector2 spawnPoint;

    private void Awake()
    {
        int levelCount = FindObjectsOfType<Level>().Length;
        Debug.Log(levelCount);
        if (levelCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        InvokeRepeating("addBonusGhost", 0, spawnTime);

    }

    public int GetEndScore()
    {
        return score;
    }

    void Update()
    {
        if (livesText && scoreText)
        UpdateLivesAndScoreText();
    }

    public void CountGhosts()
    {
        numberOfGhosts++;
    }

    public void RemoveGhost()
    {
        numberOfGhosts--;
        if (numberOfGhosts <= 0)
        {
            sceneLoader.LoadWinScene();
        }
    }

    public void LoseLife()
    {
        lives -= 1;
        if (lives <= 0)
        {
            sceneLoader.LoadLoseScene();
        }
    }

    private void UpdateLivesAndScoreText()
    {
        Debug.Log(score);
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
    }

    public void UpdateScoreGhost()
    {
        score += 20;
    }

    public void UpdateScoreBonusGhost()
    {
        score += 100;
    }

    public void RemoveTextScoreAndLives()
    {
        Destroy(livesText);
        Destroy(scoreText);
    }

    private void addBonusGhost()
    {
        var spawnPointLeft = new Vector2(-1.39f, 9.32f);
        var spawnPointRight = new Vector2(13.39f, 9.32f);

        if (Random.Range(0, 2) == 0)
        {
            spawnPoint = spawnPointLeft;
            bonusGhost.speed = 2.0f;
        } else
        {
            spawnPoint = spawnPointRight;
            bonusGhost.speed = -2.0f;
        }

        Instantiate(bonusGhost, spawnPoint, Quaternion.identity);
    }
}
