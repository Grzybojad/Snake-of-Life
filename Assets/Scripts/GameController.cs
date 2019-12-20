using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
	public GameObject applePrefab;
	public GameObject levelBoundry;
	public TextMeshProUGUI scoreText;

	private Vector3 levelSize3D;
	private Vector2 levelSize2D;
	private float spawnPosLimit = 0.8f;
	private GameObject apple;

	private int score;

	// Start is called before the first frame update
	void Start()
	{
		levelSize3D = levelBoundry.GetComponent<MeshCollider>().bounds.size;
		levelSize2D = new Vector2( levelSize3D.x, levelSize3D.z );
		apple = SpawnApple();
		score = 0;

		FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
	}

	// Update is called once per frame
	void Update()
	{
		// Debug
		if( Input.GetKeyDown( KeyCode.R ) )
			SpawnApple();

		if( apple == null )
		{
			score++;
			scoreText.text = "Score: " + score;
			apple = SpawnApple();
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

	private void OnPlayerDeath()
	{
		Time.timeScale = 0;
	}
}
