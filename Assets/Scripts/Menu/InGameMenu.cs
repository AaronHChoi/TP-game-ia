using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] GameObject menuUI;

    bool isPaused = false;

    private void Start()
    {
        menuUI.SetActive(false);
        
    }
    public void Update()
    {
        Pause();
        
    }
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
    }
    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
