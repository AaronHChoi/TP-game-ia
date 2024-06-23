using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject endScreen;
    public TextMeshProUGUI result;

    public void ChangeResult(string newText)
    {
        result.text = newText;
    }
}
