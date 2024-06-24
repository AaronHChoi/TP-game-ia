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
    string win = "Win";
    string lose = "Lose";
    [SerializeField] SizeManager sizeManager;
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
        FoodDestroy();
        UpdateSize();
    }
    public void FoodDestroy()
    {
        FooodLeft = GameObject.FindGameObjectsWithTag("Food").Length;
        FooodLeft--;
        if(FooodLeft <= 0)
        {
           EndGame(win);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }
    public void EndGame(string result)
    {
        Pause();
        Hud.endScreen.SetActive(true);
        Hud.ChangeResult(result);
    }
    private void UpdateSize()
    {
        sizeText.text = "Actual Size: " + sizeManager.playerValue.ToString();
    }
}