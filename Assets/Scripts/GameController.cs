using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject applePrefab;
	public GameObject levelBoundry;

	private Vector2 levelSize;
	private float spawnPosLimit = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
		levelSize = new Vector2( levelBoundry.transform.localScale.x, levelBoundry.transform.localScale.z );
    }

    // Update is called once per frame
    void Update()
    {
		// Debug
		if( Input.GetKeyDown( KeyCode.R ) )
			SpawnApple();
	}

	void SpawnApple()
	{
		System.Random rand = new System.Random();
		Vector3 spawnPos = new Vector3(
			(float)rand.NextDouble() * levelSize.x * 2 - levelSize.x * spawnPosLimit,
			applePrefab.transform.position.y,
			(float)rand.NextDouble() * levelSize.y * 2 - levelSize.y * spawnPosLimit
		);
		Instantiate( applePrefab, spawnPos, applePrefab.transform.rotation );
	}
}
