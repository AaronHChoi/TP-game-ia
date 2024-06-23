using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject Back;
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenCredits()
    {

        creditsPanel.SetActive(true);
        Back.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void OpenControls()
    {

        controlsPanel.SetActive(true);
        Back.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void BackButton()
    {

        Back.SetActive(false);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void QuitButton()
    {

        Application.Quit();
    }
}
