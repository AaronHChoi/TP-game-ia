using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLvl : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag ("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}