using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject applePrefab;
	public GameObject levelBoundry;

	private Vector3 levelSize3D;
	public Vector2 levelSize2D;
	private float spawnPosLimit = 0.8f;
	public GameObject apple;

	// Start is called before the first frame update
	void Start()
	{
		levelSize3D = levelBoundry.GetComponent<MeshCollider>().bounds.size;
		levelSize2D = new Vector2( levelSize3D.x, levelSize3D.z );
		apple = SpawnApple();
	}

	// Update is called once per frame
	void Update()
	{
		// Debug
		if( Input.GetKeyDown( KeyCode.R ) )
			SpawnApple();

		if( apple == null )
			apple = SpawnApple();
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
}
