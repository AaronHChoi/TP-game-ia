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
    public void Cam()
    {
        mouseX += rotSpeed * Input.GetAxis("Mouse X");
        mouseY -= rotSpeed* Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, rotMin, rotMax);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        player.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }
    private void LateUpdate()
    {
        Cam();

        transform.position = posTP.position;
        transform.LookAt(player);
    }
}