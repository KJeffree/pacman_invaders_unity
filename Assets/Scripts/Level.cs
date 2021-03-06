﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Level : MonoBehaviour
{

    List<PlayerScore> scores = new List<PlayerScore>();
    SceneLoader sceneLoader;
    [SerializeField] AudioClip startGame;

    [SerializeField] AudioClip newWave;

    [SerializeField] int numberOfGhosts = 0;

    [SerializeField] int lives = 3;

    [SerializeField] GameObject[] livesImages;
    GameObject lifeImage;
    [SerializeField] Sprite blankImage;

    [SerializeField] GameObject saveScoreButton;

    [SerializeField] TMP_InputField initialTextField;

    int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] TextMeshProUGUI readyText;

    public BonusGhost bonusGhost;
    float spawnTime = 15;
    Vector2 spawnPoint;

    [SerializeField] Ghost[] ghosts;

    [SerializeField] GameObject ghostsPrefab;

    [SerializeField] Transform ghostsSpwan;

    GameObject ghostObject;

    GhostHiveMind ghostHiveMind;

    int wave = 0;

    Pacman pacman;

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
        saveScoreButton.SetActive(false);

        sceneLoader = FindObjectOfType<SceneLoader>();
    
        InvokeRepeating("addBonusGhost", 0, spawnTime);

        ghosts = FindObjectsOfType<Ghost>();

        AudioSource.PlayClipAtPoint(startGame, Camera.main.transform.position);

        ghostHiveMind = FindObjectOfType<GhostHiveMind>();

        Invoke("AssignFruit", 2);

        lifeImage = livesImages[0];

        highscoreText.text = null;

        readyText.text = "READY!";

        StartCoroutine(WaitAndRemoveText());

        pacman = FindObjectOfType<Pacman>();

    }

    IEnumerator WaitAndRemoveText()
    {
        yield return new WaitForSeconds(4);
        readyText.text = null;
    }

    static int SortByScore(PlayerScore p1, PlayerScore p2)
    {
        return p1.score.CompareTo(p2.score);
    }

    public void LoadHighScores()
    {
        saveScoreButton.SetActive(true);
        // File.Delete(Application.persistentDataPath + "/playerScores.dat");
        
        if(File.Exists(Application.persistentDataPath + "/playerScores.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Open);
            if (file.Length != 0)
            {
                PlayerScores playerScores = (PlayerScores)bf.Deserialize(file);
                file.Close();

                scores = playerScores.scores;
                String text = null;

                if (scores.Count > 0)
                {
                    scores.Sort(SortByScore);
                    scores.Reverse();

                    for (int i=0; i < 4; i++)
                    {
                        if (i < scores.Count)
                        {
                            string highscoreText = scores[i].score.ToString() + " : " + scores[i].initials.ToString() + "\n";
                            text += highscoreText;
                        }
                    }

                    highscoreText.text = text;
                }
                Debug.Log("Loaded!");
            }
            
        }
        
    }

    public void SaveScore()
    {
        initialTextField = FindObjectOfType<TMP_InputField>();
        if (initialTextField.text.Length == 3)
        {
            var initials = initialTextField.text.ToString();
            
            PlayerScore playerScore = new PlayerScore();
            playerScore.initials = initials;
            playerScore.score = score;

            scores.Add(playerScore);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerScores.dat");

            PlayerScores playerScores = new PlayerScores();
            playerScores.scores = scores;

            bf.Serialize(file, playerScores);
            file.Close();

            LoadHighScores();

            Debug.Log("Saved!");
            saveScoreButton.SetActive(false);
        }
    }

    void OnDisable()
    {
        highscoreText.text = null;
        saveScoreButton.SetActive(false);
    }

    public void IncreaseLives()
    {
        if (lives < 3)
        {
            livesImages[lives].GetComponent<UnityEngine.UI.Image>().sprite = lifeImage.GetComponent<UnityEngine.UI.Image>().sprite;
            lives++;

        }
    }

    public void AssignFruit()
    {
        ghostHiveMind.AssignFruit();
    }

    public int GetEndScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetWaveNumber()
    {
        return wave;
    }

    void Update()
    {
        if (scoreText)
        {
            UpdateScoreText();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            CancelInvoke();
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
        ghostHiveMind.IncreaseShooting();
    }

    public void LoseLife()
    {
        lives -= 1;
        if (lives <= 0)
        {
            pacman.Die();
            sceneLoader.LoadLoseScene();
        }
        livesImages[lives].GetComponent<UnityEngine.UI.Image>().sprite = blankImage;
    }

     private void NextGhostWave()
    {
        pacman.PreventShooting();
        Pill[] pills = FindObjectsOfType<Pill>();
        foreach (Pill pill in pills)
        {
            Destroy(pill);
        }
        Vector3 startPosition= new Vector3(0.0f, 5.5f, 0.0f);
        ghostObject = Instantiate(ghostsPrefab, startPosition, ghostsSpwan.rotation);
        StartCoroutine(MoveToPosition(ghostObject.GetComponent<Transform>(), new Vector3(0.0f, 0.0f, 0.0f), 4));
        AudioSource.PlayClipAtPoint(newWave, Camera.main.transform.position);
        Invoke("AssignFruit", 2);
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScoreGhost()
    {
        score += 20;
    }

    public void UpdateScoreBonusGhost()
    {
        score += 150;
    }

    public void UpdateScoreCherry()
    {
        score += 100;
    }

    public void RemoveTextScoreAndLives()
    {
        Destroy(scoreText);
    }

    private void addBonusGhost()
    {
        var spawnPointLeft = new Vector2(-1.39f, 9.32f);
        var spawnPointRight = new Vector2(13.39f, 9.32f);

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            spawnPoint = spawnPointLeft;
            BonusGhostSpeedSet(2.0f, 0.1f);
        } else
        {
            spawnPoint = spawnPointRight;
            BonusGhostSpeedSet(-2.0f, -0.1f);
        }

        Instantiate(bonusGhost, spawnPoint, Quaternion.identity);
    }

    private void BonusGhostSpeedSet(float speed, float change)
    {
        bonusGhost.speed = speed;
            int x = 0;
            while (x < wave)
            {
                bonusGhost.speed += change;
                x++;
            } 
    }

}

[Serializable]
class PlayerScore
{
    public String initials;
    public int score;
}

[Serializable]
class PlayerScores
{
    public List<PlayerScore> scores;
}