using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject lostText;
    [SerializeField] GameObject menuPanelPanel;
    [SerializeField] GameObject menuPanel;
    bool isPaused;
    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.CompareTag("player"))
        {
            Win();
        }
    }
    void Win()
    {
        menuPanel.SetActive(true);
        menuPanelPanel.SetActive(true);
        winText.SetActive(true);
        TogglePause();
    }
    public void Lost() 
    {
        menuPanel.SetActive(true);
        menuPanelPanel.SetActive(true);
        lostText.SetActive(true);
        TogglePause();
    }
    void TogglePause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0f;
            menuPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            menuPanel.SetActive(false);
        }
    }
}
