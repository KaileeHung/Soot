using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject playScreen;
    public int scoreToWin;
    private AudioSource audioSource;
    private bool deathSoundTriggered = false;

    [ContextMenu("Increase Score")]
    public void addScore() {
        playerScore += 1;
        scoreText.text = playerScore + "/" + scoreToWin;
        if (playerScore == scoreToWin) {
            StartCoroutine(FreezeForOneSecond("win"));
        }
    }

    public void gameOver() {
        deathSoundTriggered = true;
        if (deathSoundTriggered) {
            audioSource.Play();
        }
        StartCoroutine(FreezeForOneSecond("lose"));
    }

    public void restartGame() {
        Debug.Log("Game restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        scoreText.text = playerScore + "/" + scoreToWin;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            Debug.LogError("Audio Source component not found!");
        }
    }
    IEnumerator FreezeForOneSecond(string state)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;

        switch(state) {
            case "win" :
                winScreen.SetActive(true);
                break;

            case "lose":
                gameOverScreen.SetActive(true);
                break;

            default:
                break;
        }
        playScreen.SetActive(false);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
