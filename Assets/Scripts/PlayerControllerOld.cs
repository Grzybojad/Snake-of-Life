using System;
using UnityEngine;

public class PlayerControllerOld : MonoBehaviour
{
	public float speed;
	public float turningSpeed;
	public uint startingLength;

	public BodyControllerOld bodyPrefab;
	private BodyControllerOld firstPart;

	private Rigidbody rb;

	private AudioSource audioSource;
	public AudioClip bite_sfx;
	public AudioClip death_sfx;

	public event Action deathEvent;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		for( int i = 0; i < startingLength; i++ )
			AddPart();
	}

	private void FixedUpdate()
	{
		Movement();
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
		int nrOfPartsToIgnore = 2;
		BodyControllerOld part = firstPart;
		for( int i = 0; i < nrOfPartsToIgnore; i++ )
		{
			if( other.gameObject == part ) return;
			if( part.nextPart != null )
				part = part.nextPart;
			else
				return;
		}
		if( deathEvent != null )
		{
			audioSource.PlayOneShot( death_sfx );
			deathEvent();
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

	void AddPart()
	{
		if( firstPart == null )
		{
			firstPart = Instantiate( bodyPrefab, transform.position, transform.rotation );
			firstPart.followTarget = this.gameObject;
		}
		else
		{
			BodyControllerOld part = firstPart;
			while( part.nextPart != null )
				part = part.nextPart;

			part.nextPart = Instantiate( bodyPrefab, part.transform.position, part.transform.rotation );
			part.nextPart.followTarget = part.gameObject;
		}
			
	}
}
