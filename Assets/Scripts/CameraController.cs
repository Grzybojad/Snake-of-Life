using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject focusObject;
    public Vector3 cameraOffset;

    // Camera boundry
    public Vector2 minCameraValue;
    public Vector2 maxCameraValue;

    private Vector3 focusPos;

    void Update()
    {
		focusPos = focusObject.transform.position;
    
		Vector3 pos = focusPos + cameraOffset;

        // Clamp camera position
        transform.position = new Vector3(
            Mathf.Clamp( pos.x, minCameraValue.x, maxCameraValue.x ),
            pos.y,
            Mathf.Clamp( pos.z, minCameraValue.y, maxCameraValue.y )
        );
    }
}
