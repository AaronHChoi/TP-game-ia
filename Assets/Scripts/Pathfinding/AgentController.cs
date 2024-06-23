using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentController : MonoBehaviour
{
    public DuckAlphaController duck;
    public float radius = 3f;
    public LayerMask maskObs;
    public LayerMask maskNodes;
    public Node target;
    public Box box;

    public void RunThetaStar()
    {
        var start = GetNearNode(duck.transform.position);
        if (start == null) return;
        List<Node> path = ThetaStar.Run(start, GetConnections, IsSatiesfies, GetCost, Heuristic, InView);
        duck.GetStateWaypoints.SetWayPoints(path);
        //box.SetWayPoints(path);
    }
    bool InView(Node grandParent, Node child)
    {
        Debug.Log("RAY");
        return InView(grandParent.transform.position, child.transform.position);
    }
    bool InView(Vector3 a, Vector3 b)
    {
        //a->b  b-a
        Vector3 dir = b - a;
        return !Physics.Raycast(a, dir.normalized, dir.magnitude, maskObs);
    }
    float Heuristic(Node current)
    {
        float heuristic = 0;
        float multiplierDistance = 1;
        heuristic += Vector3.Distance(current.transform.position, target.transform.position) * multiplierDistance;
        return heuristic;
    }
    //float Heuristic(Vector3 current)
    //{
    //    float heuristic = 0;
    //    float multiplierDistance = 1;
    //    heuristic += Vector3.Distance(current, box.transform.position) * multiplierDistance;
    //    return heuristic;
    //}
    float GetCost(Node parent, Node child)
    {
        float cost = 0;
        float multiplierDistance = 1;
        //float multiplierTrap = 200;
        cost += Vector3.Distance(parent.transform.position, child.transform.position) * multiplierDistance;
        //if (child.hasTrap)
        //{
        //    cost += multiplierTrap;
        //}
        return cost;
    }
    //float GetCost(Vector3 parent, Vector3 child)
    //{
    //    float cost = 0;
    //    float multiplierDistance = 1;
    //    cost += Vector3.Distance(parent, child) * multiplierDistance;
    //    return cost;
    //}
    Node GetNearNode(Vector3 pos)
    {
        var nodes = Physics.OverlapSphere(pos, radius, maskNodes);
        Node nearNode = null;
        float nearDistance = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            var currentNode = nodes[i];
            var dir = currentNode.transform.position - pos;
            float currentDistance = dir.magnitude;
            if (nearNode == null || currentDistance < nearDistance)
            {
                if (!Physics.Raycast(pos, dir.normalized, currentDistance, maskObs))
                {
                    nearNode = currentNode.GetComponent<Node>();
                    nearDistance = currentDistance;
                }
            }
        }
        return nearNode;
    }
    List<Node> GetConnections(Node current)
    {
        return current.neightbourds;
    }
    bool IsSatiesfies(Node current)
    {
        return current == target;
    }
    //bool IsSatiesfies(Vector3 current)
    //{
    //    return Vector3.Distance(current, box.transform.position) < 2 && InView(current, box.transform.position);
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(duck.transform.position, radius);
    }
}
