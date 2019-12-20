using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject applePrefab;
	public GameObject levelBoundry;

	private Vector3 levelSize3D;
	private Vector2 levelSize2D;
	private float spawnPosLimit = 0.8f;
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
		levelSize3D = levelBoundry.GetComponent<MeshCollider>().bounds.size;
		levelSize2D = new Vector2( levelSize3D.x, levelSize3D.z );
		apple = SpawnApple();
		score = 0;

		highscore = PlayerPrefs.GetInt( "Highscore" );

		// Add a listener for the player death event
		FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;

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
		if( apple == null )
		{
			score++;
			newScoreEvent?.Invoke( score );
			apple = SpawnApple();
		}
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

	GameObject SpawnApple()
	{
		System.Random rand = new System.Random();
		Vector3 spawnPos = new Vector3(
			((float)rand.NextDouble() - 0.5f) * levelSize2D.x * spawnPosLimit,
			applePrefab.transform.position.y,
			((float)rand.NextDouble() - 0.5f) * levelSize2D.y * spawnPosLimit
		);
		return Instantiate( applePrefab, spawnPos, applePrefab.transform.rotation );
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
