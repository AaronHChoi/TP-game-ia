using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostModel : MonoBehaviour
{
    bool _alert;
    public bool Alert
    {
        set { _alert = value; }
        get { return _alert; }
    }
}
