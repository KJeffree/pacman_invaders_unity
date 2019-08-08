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

    [SerializeField] Ghost[] ghosts;

    [SerializeField] GameObject ghostsPrefab;

    [SerializeField] Transform ghostsSpwan;

    int wave = 0;

    private void Awake()
    {
        int levelCount = FindObjectsOfType<Level>().Length;
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

        ghosts = FindObjectsOfType<Ghost>();

    }

    public int GetEndScore()
    {
        return score;
    }

    void Update()
    {
        if (livesText && scoreText)
        {
            UpdateLivesAndScoreText();
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
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
            NextGhostWave();
            ghosts = FindObjectsOfType<Ghost>();
            wave += 1;
            IncreaseGhostSpeed();
        }
    }

    private void IncreaseGhostSpeed()
    {
        int x = 0;
        while (x < wave)
        {
            foreach (var ghost in ghosts)
            {
                ghost.IncreaseSpeed();
            }
            x++;
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

     private void NextGhostWave()
    {
        Instantiate(ghostsPrefab, ghostsSpwan.position, ghostsSpwan.rotation);
    }

    private void UpdateLivesAndScoreText()
    {
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
