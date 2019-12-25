using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	//public GameObject applePrefab;
	//public GameObject levelBoundry;

	//private Vector3 levelSize3D;
	//private Vector2 levelSize2D;
	//private float spawnPosLimit = 0.8f;
	private GameObject apple;

	public int score;
	public int highscore;

	public delegate void ScoreDelegate( int score );
	public event ScoreDelegate newScoreEvent;
	public event ScoreDelegate newHighscoreEvent;

	enum GameState
	{
		playing,
		paused,
		gameOver
	}
	GameState _gameState;

	// Start is called before the first frame update
	void Awake()
	{
		score = 0;

		highscore = PlayerPrefs.GetInt( "Highscore" );

		// Add listeners
		FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
		FindObjectOfType<AppleSpawner>().appleCollectEvent += OnAppleCollectEvent;

		_gameState = GameState.playing;
	}

	private void Start()
	{
		if( highscore > 0 )
			newHighscoreEvent?.Invoke( highscore );
	}

	// Update is called once per frame
	void Update()
	{
		switch( _gameState )
		{
			case GameState.playing:
				PlayingUpdate();
				break;
			case GameState.paused:

				break;
			case GameState.gameOver:
				GameOverUpdate();
				break;
		}
	}

	void PlayingUpdate()
	{
		
	}

	void GameOverUpdate()
	{
		// Temporary
		if( Input.GetKeyDown( KeyCode.R ) )
		{
			Time.timeScale = 1;
			PlayerPrefs.Save();

			SceneManager.LoadScene( 0 );
		}
	}

	void OnAppleCollectEvent()
	{
		score++;
		newScoreEvent?.Invoke( score );
	}

	void OnPlayerDeath()
	{
		Time.timeScale = 0;

		if( score > highscore )
		{
			highscore = score;
			newHighscoreEvent?.Invoke( highscore );
			PlayerPrefs.SetInt( "Highscore", highscore );
		}

		_gameState = GameState.gameOver;
	}
}
