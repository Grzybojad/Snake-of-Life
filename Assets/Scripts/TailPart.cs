using System.Collections.Generic;
using UnityEngine;

public class TailPart : MonoBehaviour
{
	public Transform parent;
    public float margin;
    

	public LinkedListNode<Vector3> FollowTrail( LinkedList<Vector3> path )
    {
		var nextNode = path.Last;
		Vector3 partPos = nextNode.Value;

		float distanceToParent = (partPos - parent.position).magnitude - margin;
		float distanceToNextNode = (partPos - nextNode.Value).magnitude;

		// Iterate through the path nodes to place the tail part in the correct place
		while( distanceToNextNode < distanceToParent && nextNode != null )
        {
			partPos = nextNode.Value;
            distanceToParent = (partPos - parent.position).magnitude - margin;

            nextNode = nextNode.Previous;
            if( nextNode != null )
                distanceToNextNode = (partPos - nextNode.Value).magnitude;
        }
		transform.position = partPos;

		transform.LookAt( nextNode.Value );
		transform.Translate( Vector3.forward * distanceToParent );

		return nextNode;
    }
}
