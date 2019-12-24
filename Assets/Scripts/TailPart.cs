using System.Collections.Generic;
using UnityEngine;

public class TailPart : MonoBehaviour
{
    public float margin;

    public Transform parent;

	public float distanceToParent;
	public float distanceToNextNode;


	public LinkedListNode<Vector3> FollowTrail( LinkedList<Vector3> path )
    {
		var nextNode = path.Last;
		Vector3 partPos = nextNode.Value;

		distanceToParent = (partPos - parent.position).magnitude - margin;
        distanceToNextNode = (partPos - nextNode.Value).magnitude;

		while( distanceToNextNode < distanceToParent && nextNode != null )
        {
			partPos = nextNode.Value;
            distanceToParent = (partPos - parent.position).magnitude - margin;

            nextNode = nextNode.Previous;
            if( nextNode != null )
                distanceToNextNode = (partPos - nextNode.Value).magnitude;
        }
		transform.position = partPos;
		transform.LookAt( parent );

		// Need to do some "interpolation here to make the movement less jittery I guess?
		//transform.Translate( Vector3.forward * distanceToParent );

		return nextNode;
    }
}
