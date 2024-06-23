using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] Text sizeText;

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
        FooodLeft = GameObject.FindGameObjectsWithTag("Food").Length;
        FoodDestroy();
        UpdateSize();
    }
    public void FoodDestroy()
    {
        FooodLeft--;
        if(FooodLeft <= 0)
        {
           EndGame(win);
        }
    }
    public void EndGame(string result)
    {
        Hud.endScreen.SetActive(true);
        Hud.ChangeResult(result);
    }

    private void UpdateSize()
    {
        sizeText.text = "Tamaño actual= " + sizeManager.playerValue.ToString();
    }
}