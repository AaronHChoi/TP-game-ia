using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameManager.Instance.UnPause();
        isPaused = false;
    }
    public void PauseGame()
    {
        menuUI.SetActive(true);
        GameManager.Instance.Pause();
        isPaused = true;
    }
    public void GoToMenu()
    {
        GameManager.Instance.UnPause();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {

        Application.Quit();
    }
}
