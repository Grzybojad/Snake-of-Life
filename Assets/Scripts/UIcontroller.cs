using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private GameController gameController;
    private int currentScore;
    private int currentHighscore;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Game Controller object
        gameController = FindObjectOfType<GameController>();

        // Event listeners
        FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
        FindObjectOfType<GameController>().newScoreEvent += OnScoreChange;
        FindObjectOfType<GameController>().newHighscoreEvent += OnHighscoreChange;
    }

    private void OnPlayerDeath()
    {
        gameOverScreen.SetActive( true );
        finalScoreText.text = "Your score: " + currentScore;
    }

    private void OnScoreChange( int score )
    {
        currentScore = score;
        scoreText.text = "Score: " + score;
    }

    private void OnHighscoreChange( int highscore )
    {
        currentHighscore = highscore;
        highscoreText.text = "Highscore: " + highscore;
    }
}
