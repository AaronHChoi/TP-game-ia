using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string loadScene1;
    [SerializeField] string loadScene2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(loadScene1);
    }
    public void LoadSceneByName2()
    {
        SceneManager.LoadScene(loadScene2);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
