using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTest : MonoBehaviour
{
    public Transform posTP;

    //Camara TP
    public float rotSpeed; 
    public float rotMin, rotMax; 
    float mouseX, mouseY;
    public Transform target, player;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        mouseX += rotSpeed * Input.GetAxis("Mouse X");
        mouseY -= rotSpeed * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, rotMin, rotMax);
    }
    private void LateUpdate()
    {
        player.rotation = Quaternion.Euler(0f, mouseX, 0f);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);

        transform.position = posTP.position;
        transform.LookAt(player);
    }
}