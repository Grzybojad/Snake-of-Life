using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControllerOld : MonoBehaviour
{
	public GameObject followTarget;
	public BodyControllerOld nextPart;

	//public float speed;
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
			transform.Translate( Vector3.forward * ((transform.position - targetPos).magnitude - distance) );
		//transform.Translate( Vector3.forward * speed * Time.deltaTime );
	}
}
