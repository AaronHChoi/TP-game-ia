using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSize : MonoBehaviour
{
    public float currentSize = 1.0f;

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
