using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : ITreeNode
{
    Dictionary<ITreeNode, float> _dic;

    public RandomNode(Dictionary<ITreeNode, float> dic)
    {
        _dic = dic;
    }

    public void Execute()
    {
        MyRandoms.Roulette(_dic).Execute();
    }
}
