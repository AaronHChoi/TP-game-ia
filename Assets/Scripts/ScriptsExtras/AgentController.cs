using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public CrashController crash;
    public float radius = 3;
    public LayerMask maskNodes;
    public LayerMask maskObs;
    public Box box;
    public Node target;
    public void RunBFS()
    {
        var start = GetNearNode(crash.transform.position);
        if (start == null) return;
        List<Node> path = BFS.Run(start, GetConnections, IsSatiesfies);
        crash.GetStateWaypoints.SetWayPoints(path);
        box.SetWayPoints(path);
    }
    public void RunDFS()
    {
        var start = GetNearNode(crash.transform.position);
        if (start == null) return;
        List<Node> path = DFS.Run(start, GetConnections, IsSatiesfies);
        crash.GetStateWaypoints.SetWayPoints(path);
        box.SetWayPoints(path);
    }
    public void RunDijkstra()
    {
        var start = GetNearNode(crash.transform.position);
        if (start == null) return;
        List<Node> path = Dijkstra.Run(start, GetConnections, IsSatiesfies, GetCost);
        crash.GetStateWaypoints.SetWayPoints(path);
        box.SetWayPoints(path);
    }
    public void RunAStar()
    {
        var start = GetNearNode(crash.transform.position);
        if (start == null) return;
        List<Node> path = AStar.Run(start, GetConnections, IsSatiesfies, GetCost, Heuristic);
        crash.GetStateWaypoints.SetWayPoints(path);
        box.SetWayPoints(path);
    }
    float Heuristic(Node current)
    {
        float heuristic = 0;
        float multiplierDistance = 1;
        heuristic += Vector3.Distance(current.transform.position, target.transform.position) * multiplierDistance;
        return heuristic;
    }
    float GetCost(Node parent, Node child)
    {
        float cost = 0;
        float multiplierDistance = 1;
        float multiplierTrap = 200;
        cost += Vector3.Distance(parent.transform.position, child.transform.position) * multiplierDistance;
        if (child.hasTrap)
        {
            cost += multiplierTrap;
        }
        return cost;
    }
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(crash.transform.position, radius);
    }
}
