using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int FoodLeft;
    public HUD Hud;
    int lives = 3;
    string win = "Win";
    string lose = "Lose";
    [SerializeField] SizeManager Player;
    [SerializeField] Text SizeText;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        FoodLeft = GameObject.FindGameObjectsWithTag("Food").Length;
    }

    private void Update()
    {
       
        UpdateSize();
        //FoodEaten();
    }
    public void FoodEaten()
    {
        FoodLeft--;
        if(FoodLeft <= 0)
        {
           EndGame(win);
        }
    }
    public void ResetScene()
    {
        if(lives <= 0)
        {
            EndGame(lose);
        }
    }
   
    public void EndGame(string result)
    {
        Hud.endScreen.SetActive(true);
        Hud.ChangeResult(result);
    }

    private void UpdateSize()
    {
        SizeText.text = "Current Size: " + Player.playerValue.ToString();
    }
}