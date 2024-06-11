using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS
{
    public static List<T> Run<T>(T start, Func<T, List<T>> getConnections, Func<T, bool> isSatisfies, int watchdog = 500)
    {
        Stack<T> pending = new Stack<T>();
        HashSet<T> visited = new HashSet<T>();
        Dictionary<T, T> parents = new Dictionary<T, T>();

        pending.Push(start);
        while (pending.Count > 0)
        {
            Debug.Log("DFS");
            watchdog--;
            if (watchdog <= 0) break;
            T current = pending.Pop();
            if (isSatisfies(current))
            {
                var path = new List<T>();
                path.Add(current);
                while (parents.ContainsKey(path[path.Count - 1]))
                {
                    path.Add(parents[path[path.Count - 1]]);
                }
                path.Reverse();
                return path;
            }
            visited.Add(current);
            List<T> connections = getConnections(current);
            for (int i = 0; i < connections.Count; i++)
            {
                T child = connections[i];
                if (visited.Contains(child)) continue;
                pending.Push(child);
                parents[child] = current;
            }
        }
        return new List<T>();
    }
}
