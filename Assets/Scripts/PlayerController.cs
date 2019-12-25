using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float turningSpeed;
	public uint startingLength;

	public TailPart bodyPrefab;
	LinkedList<Vector3> path;
	List<TailPart> tail;

	private Rigidbody rb;

	private float pathNodeSpacing = 0.1f;

	private AudioSource audioSource;
	public AudioClip bite_sfx;
	public AudioClip death_sfx;

	public event Action deathEvent;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		path = new LinkedList<Vector3>();
		InitPath();
		tail = new List<TailPart>();

		for( int i = 0; i < startingLength; i++ )
			AddPart();
	}

	void Update()
	{
		// Not sure if this should be here or in FixedUpdate
		if( tail.Count > 1 )
		{
			MoveParts();
		}
	}

	void FixedUpdate()
	{
		Movement();

		// Only add a new path node if the player has moved away from the last one
		if( (transform.position - path.First.Value).magnitude > pathNodeSpacing )
			path.AddFirst( transform.position );
	}

	// Debug draw path
	void OnDrawGizmosSelected()
	{
		// Drawing the snake path in scene view
		if( Application.isPlaying )
		{
			foreach( Vector3 pos in path )
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere( pos, 0.1f );
			}
		}
	}

	private void OnTriggerEnter( Collider other )
	{
		// Triggered an apple collider
		if( other.tag == "Collectable" )
		{
			Destroy( other.gameObject );
			audioSource.PlayOneShot( bite_sfx );
			AddPart();
		}

		// Snake collided with itself
		if( other.tag == "Player" )
		{
			HandlePlayerCollision( other );
		}
	}

	private void HandlePlayerCollision( Collider other )
	{
		// Don't collide with the parts that make up the starting length
		for( int i = 0; i < startingLength && i < tail.Count; i++ )
		{
			if( other.transform.parent == tail[ i ].transform ) return;
		}

		// Invoke the death event, so that other scripts can react to it
		if( deathEvent != null )
		{
			audioSource.PlayOneShot( death_sfx );
			deathEvent();
		}
	}


	void Movement()
	{
		// TODO: replace with input manager
		float horizontalInput = 0;
		if( Input.GetKey( KeyCode.LeftArrow ) ) horizontalInput = -1;
		if( Input.GetKey( KeyCode.RightArrow ) ) horizontalInput = 1;

		Quaternion nextRot = Quaternion.Euler( transform.eulerAngles + transform.up * horizontalInput * turningSpeed * Time.fixedDeltaTime );
		rb.MoveRotation( nextRot );

		rb.velocity = Vector3.zero;

		// Speed boost
		float speedMultiplier = 1.0f;
		if( Input.GetKey( KeyCode.Space ) )
			speedMultiplier = 1.6f;

		rb.AddForce( transform.forward * speed * speedMultiplier * Time.fixedDeltaTime, ForceMode.Impulse );
	}

	void MoveParts()
	{
		var lastNode = path.First;
		foreach( TailPart part in tail )
		{
			lastNode = part.FollowTrail( path );
		}

		// Cut off tail if it's too long
		while( lastNode != path.Last )
		{
			path.RemoveLast();
		}
	}

	void InitPath()
	{
		// Initialize a path that the starting snake pieces can align to
		for( int i = 0; i < startingLength/pathNodeSpacing; i++ )
		{
			path.AddFirst( transform.position + Vector3.back * i * pathNodeSpacing );
		}
	}

	void AddPart()
	{
		TailPart newPart;
		if( tail.Count == 0 )
		{
			newPart = Instantiate( bodyPrefab, transform.position, transform.rotation );
		}
		else
		{
			Vector3 newPartPos = tail[ tail.Count - 1 ].transform.position;
			Quaternion newPartRot = tail[ tail.Count - 1 ].transform.rotation;
			newPart = Instantiate( bodyPrefab, newPartPos, newPartRot );
		}
		if( newPart != null ) tail.Add( newPart );
		else Debug.Log( "Failed to instantiate a new part!" );

		if( tail.Count > 1 )
			tail[ tail.Count - 1 ].parent = tail[ tail.Count - 2 ].transform;
		else
			tail[ tail.Count - 1 ].parent = this.transform;
	}
}
