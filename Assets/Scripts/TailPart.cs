using System.Collections.Generic;
using UnityEngine;

public class TailPart : MonoBehaviour
{
    public float margin;

    public Transform parent;

    public LinkedListNode<Vector3> FollowTrail( LinkedList<Vector3> path )
    {
        float distanceToParent = (transform.position - parent.position).magnitude - margin;
        var nextNode = path.Last;
        float distanceToNextNode = (transform.position - nextNode.Value).magnitude;

        while( distanceToNextNode < distanceToParent && nextNode != null )
        {
            transform.LookAt( parent );
            transform.position = nextNode.Value;
            distanceToParent = (transform.position - parent.position).magnitude - margin;

            nextNode = nextNode.Previous;
            if( nextNode != null )
                distanceToNextNode = (transform.position - nextNode.Value).magnitude;
        }
        transform.LookAt( parent );
        transform.Translate( Vector3.forward * distanceToParent );

        return nextNode;
    }
}
