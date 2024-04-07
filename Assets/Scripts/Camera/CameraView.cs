using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public GameObject alertUI;
    IAlert _alert;
    private void Awake()
    {
        _alert = GetComponent<IAlert>();
    }
    private void Update()
    {
        alertUI.SetActive(_alert.Alert);
    }
}
