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
        FindObjectOfType<GameController>().pauseGameEvent += OnPauseGame;
        FindObjectOfType<GameController>().unpauseGameEvent += OnUnpauseGame;
        FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
        FindObjectOfType<GameController>().newScoreEvent += OnScoreChange;
        FindObjectOfType<GameController>().newHighscoreEvent += OnHighscoreChange;
    }

    void OnPauseGame()
    {
        pauseScreen.SetActive( true );
    }
    
    void OnUnpauseGame()
    {
        pauseScreen.SetActive( false );
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
