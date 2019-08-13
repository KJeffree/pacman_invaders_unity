using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLoseScene()
    {
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(2);
        CancelInvoke();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<Level>().ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
