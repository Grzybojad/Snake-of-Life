using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
	public GameObject followTarget;
	public BodyController nextPart;

	public float speed;
	public float distance;

	private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		targetPos = followTarget.transform.position;

		transform.LookAt( targetPos );

		if( (transform.position - targetPos).magnitude > distance )
			transform.Translate( Vector3.forward * speed * Time.deltaTime );
	}
}
