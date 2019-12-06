using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
	public GameObject focusObject;

	private Vector3 focusPos;
	private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
		focusPos = focusObject.transform.position;
		cameraOffset = transform.position - focusPos;
    }

    // Update is called once per frame
    void Update()
    {
		focusPos = focusObject.transform.position;
		transform.position = new Vector3( focusPos.x, 0, focusPos.z ) + cameraOffset;//+ cameraOffset;
    }
}
