using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float turningSpeed;

	public BodyController bodyPrefab;

	private BodyController firstPart;

    void Start()
    {

    }

    void Update()
    {
		Movement();

		// Debug
		if( Input.GetKeyDown( KeyCode.Space ) )
			AddPart();
	}


	void Movement()
	{
		float horizontalInput = Input.GetAxis( "Horizontal" );
		transform.Translate( Vector3.forward * speed * Time.deltaTime );
		transform.Rotate( Vector3.up, horizontalInput * turningSpeed * Time.deltaTime );
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
