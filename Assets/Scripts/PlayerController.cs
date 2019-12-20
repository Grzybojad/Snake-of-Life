using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float turningSpeed;
	public uint startingLength;

	public BodyController bodyPrefab;
	private BodyController firstPart;

	private Rigidbody rb;

    void Start()
    {
		rb = GetComponent<Rigidbody>();

		for( int i = 0; i < startingLength; i++ )
			AddPart();
	}

    void Update()
    {

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
			AddPart();
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
		rb.AddForce( transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse );
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
			BodyController part = firstPart;
			while( part.nextPart != null )
				part = part.nextPart;

			part.nextPart = Instantiate( bodyPrefab, part.transform.position, part.transform.rotation );
			part.nextPart.followTarget = part.gameObject;
		}
			
	}
}
