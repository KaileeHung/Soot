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
    public AudioSource deathSound;
    public AudioSource happySound1;
    public AudioSource happySound2;
    private AudioSource[] happySounds;
    private bool gameEnd = false;

    [ContextMenu("Increase Score")]
    public void addScore() {
        happySounds[UnityEngine.Random.Range(0, 2)].Play();
        playerScore += 1;
        scoreText.text = playerScore + "/" + scoreToWin;
        if (playerScore == scoreToWin) {
            StartCoroutine(FreezeForOneSecond("win"));
        }
    }

    public void gameOver() {
        deathSound.Play();
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

        happySounds = new AudioSource[] { happySound1, happySound2 };
        if (deathSound == null || happySound1 == null || happySound2 == null) {
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

    // // Update is called once per frame
    void Update()
    {
        if (gameEnd && Input.anyKeyDown) {
            gameEnd = false;
            restartGame();
        }
    }
}
