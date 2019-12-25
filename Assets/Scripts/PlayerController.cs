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

	private Vector3 frameStartPosition;

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
		frameStartPosition = transform.position;

		Movement();

		path.AddFirst( transform.position );
	}

	// Debug draw path
	void OnDrawGizmosSelected()
	{
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
		if( other.tag == "Collectable" )
		{
			Destroy( other.gameObject );
			audioSource.PlayOneShot( bite_sfx );
			AddPart();
		}

		if( other.tag == "Player" )
		{
			HandlePlayerCollision( other );
		}
	}

	private void HandlePlayerCollision( Collider other )
	{
		int nrOfPartsToIgnore = 5;
		for( int i = 0; i < nrOfPartsToIgnore && i < tail.Count; i++ )
		{
			if( other.transform.parent == tail[ i ].transform ) return;
		}
		if( deathEvent != null )
		{
			audioSource.PlayOneShot( death_sfx );
			deathEvent();
			Debug.Log( "Died to: " + other.name );
		}
	}


	void Movement()
	{
		float horizontalInput = 0;
		if( Input.GetKey( KeyCode.LeftArrow ) ) horizontalInput = -1;
		if( Input.GetKey( KeyCode.RightArrow ) ) horizontalInput = 1;

		Quaternion nextRot = Quaternion.Euler( transform.eulerAngles + transform.up * horizontalInput * turningSpeed * Time.fixedDeltaTime );
		rb.MoveRotation( nextRot );

		rb.velocity = Vector3.zero;

		float speedMultiplier = 1.0f;
		if( Input.GetKey( KeyCode.Space ) )
			speedMultiplier = 1.6f;

		rb.AddForce( transform.forward * speed * speedMultiplier * Time.fixedDeltaTime, ForceMode.Impulse );
	}

	bool HasMoved()
	{
		return frameStartPosition != transform.position;
	}

	void MoveParts()
	{
		var lastNode = path.First;
		foreach( TailPart part in tail )
		{
			lastNode = part.FollowTrail( path );
		}

		// Nr of nodes to leave extra
		int nodeMargin = 20;
		for( int i = 0; i < nodeMargin; i++ )
			if( lastNode.Next != null)
				lastNode = lastNode.Next;

		// Cut off tail if it's too long
		while( lastNode != path.Last )
		{
			path.RemoveLast();
		}
	}

	void InitPath()
	{
		// Needs more testing
		for( int i = 0; i < 20 * startingLength; i++ )
		{
			path.AddFirst( transform.position + Vector3.back * i * 0.1f );
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
