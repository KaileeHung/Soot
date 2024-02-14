using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public int scoreToWin;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject playScreen;
    public GameObject startScreen;
    public AudioSource deathSound;
    public AudioSource happySound1;
    public AudioSource happySound2;
    private AudioSource[] happySounds;
    private bool gameEnd = false;
    private bool atStart = true;

    void Start()
    {
        // set the score counter
        playerScore = 0;
        scoreText.text = playerScore + "/" + scoreToWin;

        // set screens
        startScreen.SetActive(true);
        playScreen.SetActive(false);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);

        // check sounds
        happySounds = new AudioSource[] { happySound1, happySound2 };
        if (deathSound == null || happySound1 == null || happySound2 == null) {
            Debug.LogError("Audio Source component not found!");
        }
    }

    void Update()
    {
        // from start to play
        if (atStart && Input.anyKeyDown) {
            atStart = false;
            playGame();
        }

        // from end to restart
        if (gameEnd && Input.anyKeyDown) {
            gameEnd = false;
            restartGame();
        }
    }

    // called from MoveScript.cs
    [ContextMenu("Increase Score")]
    public void addScore() {
        happySounds[UnityEngine.Random.Range(0, 2)].Play();
        playerScore += 1;
        scoreText.text = playerScore + "/" + scoreToWin;
        if (playerScore == scoreToWin) {
            StartCoroutine(FreezeForOneSecond("win"));
        }
    }

    // called from MoveScript.cs
    public void gameOver() {
        deathSound.Play();
        StartCoroutine(FreezeForOneSecond("lose"));
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // start screen to play
    public void playGame() {
        startScreen.SetActive(false);
        playScreen.SetActive(true);
    }

    IEnumerator FreezeForOneSecond(string state)
    {
        // let the game freeze for a second so it doesn't transition too quickly
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;

        // end game with win or lose
        switch(state) {
            case "win" :
                winScreen.SetActive(true);
                gameEnd = true;
                break;

            case "lose":
                gameOverScreen.SetActive(true);
                gameEnd = true;
                break;

            default:
                break;
        }
        playScreen.SetActive(false);
    }

}
