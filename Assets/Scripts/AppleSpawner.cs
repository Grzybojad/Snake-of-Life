using System;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    private GameObject apple;

    private Collider spawnerCollider;

    public event Action appleCollectEvent;

	public float throwForce;

    // Start is called before the first frame update
    void Start()
    {
        spawnerCollider = GetComponent<MeshCollider>();

        apple = SpawnApple();
    }

    // Update is called once per frame
    void Update()
    {
        if( apple == null )
        {
            appleCollectEvent?.Invoke();
            apple = SpawnApple();
        }
    }

    GameObject SpawnApple()
    {
        Vector3 spawnerPos = spawnerCollider.transform.position;
        Vector3 spawnerSize = spawnerCollider.bounds.size;

        System.Random rand = new System.Random();
        Vector3 spawnPos = new Vector3(
            spawnerPos.x + ( (float)rand.NextDouble() - 0.5f) * spawnerSize.x,
            spawnerPos.y,
            spawnerPos.z + ( (float)rand.NextDouble() - 0.5f) * spawnerSize.z
        );
        GameObject newApple = Instantiate( applePrefab, spawnPos, applePrefab.transform.rotation );
        Vector3 randForce = new Vector3( 
            ((float)rand.NextDouble() - 0.5f ) * throwForce, 
             (float)rand.NextDouble() - 0.5f,
			((float)rand.NextDouble() - 0.5f ) * throwForce
        );

        Rigidbody newAppleRb = newApple.GetComponent<Rigidbody>();
        newAppleRb.AddForce( randForce, ForceMode.Impulse );
        newAppleRb.AddTorque( randForce, ForceMode.Impulse );

        return newApple;
    }
}
