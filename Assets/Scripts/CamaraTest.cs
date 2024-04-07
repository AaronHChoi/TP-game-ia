using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTest : MonoBehaviour
{
    [SerializeField] GameObject playertoFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playertoFollow.transform);
        
    }
}
