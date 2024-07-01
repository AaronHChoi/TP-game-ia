using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int FooodLeft;
    public HUD Hud;
    public bool camaraActive = true;
    string win = "Win";
    string lose = "Lose";
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI sizeText;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Update()
    {
        //FoodDestroy();
        WinCondition();
        UpdateSize();
    }
    public void WinCondition()
    {
        if(player.playerValue > 3.5f)
        {
            camaraActive = false;
            EndGame(win);
        }
    }
    public void FoodDestroy()
    {
        FooodLeft = GameObject.FindGameObjectsWithTag("Food").Length;
        FooodLeft--;
        //if(FooodLeft <= 0)
        //{
        //   EndGame(win);
        //}
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        camaraActive = false;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        camaraActive = true;
    }
    public void EndGame(string result)
    {
        Pause();
        Hud.endScreen.SetActive(true);
        Hud.ChangeResult(result);
    }
    private void UpdateSize()
    {
        sizeText.text = "Actual Size: " + player.playerValue.ToString();
    }
}