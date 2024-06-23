using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class MainMenu : MonoBehaviour
{
    int mainMenu = 0;
    int level1 = 1;
    public void PlayButton()
    {
        SceneManager.LoadScene(level1);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void ResetButton()
    {
        SceneManager.LoadScene(level1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}