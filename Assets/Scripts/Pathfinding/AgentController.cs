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
    public List<Node> targets;
    private Node currentTarget;
    private int currentTargetIndex = 0;
    private bool isMoving = false;
    private void Start()
    {
        if (targets.Count > 0)
        {
            currentTarget = targets[currentTargetIndex];
            RunThetaStar();
        }
        else
        {
            Debug.LogError("No targets assigned to the AgentController.");
        }
    }
    void Update()
    {
        if (isMoving)
        {
            float distance = Vector3.Distance(duck.transform.position, currentTarget.transform.position);
            Debug.Log($"Distance to target: {distance}");

            if (distance <= 1f)
            {
                isMoving = false;
                Debug.Log("Reached target, updating to next target.");
                UpdateTarget();
            }
        }
    }
    public void RunThetaStar()
    {
        var start = GetNearNode(duck.transform.position);
        if (start == null) return;
        List<Node> path = ThetaStar.Run(start, GetConnections, IsSatiesfies, GetCost, Heuristic, InView);
        duck.GetStateWaypoints.SetWayPoints(path);
        isMoving = true;
        StartCoroutine(CheckCompletion());
    }
    IEnumerator CheckCompletion()
    {
        // Wait until the duck reaches the current target
        while (Vector3.Distance(duck.transform.position, currentTarget.transform.position) > 0.1f)
        {
            yield return null;
        }
        // Update to the next target
        UpdateTarget();
    }
    void UpdateTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
        currentTarget = targets[currentTargetIndex];
        RunThetaStar();
    }

    bool InView(Node grandParent, Node child)
    {
        Debug.Log("RAY");
        return InView(grandParent.transform.position, child.transform.position);
    }
    bool InView(Vector3 a, Vector3 b)
    {
        Vector3 dir = b - a;
        return !Physics.Raycast(a, dir.normalized, dir.magnitude, maskObs);
    }
    float Heuristic(Node current)
    {
        float heuristic = 0;
        float multiplierDistance = 1;
        heuristic += Vector3.Distance(current.transform.position, currentTarget.transform.position) * multiplierDistance;
        return heuristic;
    }
    float GetCost(Node parent, Node child)
    {
        float cost = 0;
        float multiplierDistance = 1;
        cost += Vector3.Distance(parent.transform.position, child.transform.position) * multiplierDistance;
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
        return current == currentTarget;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(duck.transform.position, radius);
    }
}