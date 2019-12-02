using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float turningSpeed;

	//private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		//rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		float horizontalInput = Input.GetAxis( "Horizontal" );
		transform.Translate( Vector3.forward * speed * Time.deltaTime );
		transform.Rotate( Vector3.up, horizontalInput * turningSpeed * Time.deltaTime );
	}
}
