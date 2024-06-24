using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neightbourds;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach (Node neighbour in neightbourds)
        {
            if (neighbour != null)
            {
                Gizmos.DrawLine(transform.position, neighbour.transform.position);
            }
        }
    }
}
