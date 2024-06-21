using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    public float playerCurrentSize = 1.0f;

    public GameObject player;
    public GameObject food;

    private void OnTriggerEnter(Collider other)
    {

        Compare();
    }

    private void Compare()
    {
        SizeManager sizeManager = player.GetComponent<SizeManager>();
        FoodSize foodSize = food.GetComponent<FoodSize>();

        if (sizeManager.playerCurrentSize == foodSize.currentSize)
        {
            sizeManager.playerCurrentSize += 0.1f;
            transform.localScale = new Vector3(sizeManager.playerCurrentSize, sizeManager.playerCurrentSize, sizeManager.playerCurrentSize);
            Destroy(food);
        }
    }

}
