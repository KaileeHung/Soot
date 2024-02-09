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

    [ContextMenu("Increase Score")]
    public void addScore() {
        playerScore += 1;
        scoreText.text = playerScore + "/" + scoreToWin;
        if (playerScore == scoreToWin) {
            winScreen.SetActive(true);
            playScreen.SetActive(false);
        }
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        playScreen.SetActive(false);

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
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
