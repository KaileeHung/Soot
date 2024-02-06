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

    [ContextMenu("Increase Score")]
    public void addScore() {
        playerScore += 1;
        scoreText.text = playerScore + "/20";
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
    }

    public void restartGame() {
        Debug.Log("Game restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
