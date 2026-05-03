using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Direction { North, South, West, East}
public enum Height{Ground, High}
public class ChunkConnector : MonoBehaviour
{
    public Direction Direction;
    public Height Height;
    void OnDrawGizmos()
    {
        Gizmos.color = Direction == Direction.North ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }

}
