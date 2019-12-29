using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private GameController gameController;
    private int currentScore;

    void Awake()
    {
        // Get a reference to the Game Controller object
        gameController = FindObjectOfType<GameController>();

        // Event listeners
        FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
        FindObjectOfType<GameController>().newScoreEvent += OnScoreChange;
        FindObjectOfType<GameController>().newHighscoreEvent += OnHighscoreChange;
    }

    private void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            if( pauseScreen.activeInHierarchy )
            {
                Time.timeScale = 1;
                pauseScreen.SetActive( false );
            } 
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive( true );
            }
        }
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
        highscoreText.text = "Highscore: " + highscore;
    }
}
