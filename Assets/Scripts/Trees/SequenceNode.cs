using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : ITreeNode
{
    private readonly List<ITreeNode> _nodes;

    public SequenceNode(List<ITreeNode> nodes)
    {
        _nodes = nodes;
    }
    public void Execute()
    {
        foreach(var node in _nodes)
        {
            node.Execute();
        }
    }

}
