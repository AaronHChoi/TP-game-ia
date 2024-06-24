using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    public GameObject player;
    public float playerValue; // Adjust this value for each object
    private GameManager gameManager;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            // Get the value of the other object
            float otherValue = collision.gameObject.GetComponent<FoodSize>().currentSize;

            SizeManager sizeManager = player.GetComponent<SizeManager>();
            FoodSize foodSize = collision.GetComponent<FoodSize>();


            // Compare values
            if (playerValue >= otherValue)
            {
                playerValue += foodSize.currentSize/10;
                player.transform.localScale = new Vector3(sizeManager.playerValue, sizeManager.playerValue, sizeManager.playerValue);
                collision.gameObject.SetActive(false);

            }
            else if (playerValue < otherValue)
            {
                Debug.Log("me comieron");
                gameManager.EndGame("Lose");
            }
        }
    }

    //public float playerCurrentSize = 1.0f;

    //public GameObject player;
    //public GameObject[] foodList;

    //private void OnTriggerEnter(Collider other)
    //{
    //    SizeManager sizeManager = player.GetComponent<SizeManager>();

    //    foreach (GameObject foodObject in foodList)
    //    {
    //        FoodSize foodSize = foodObject.GetComponent<FoodSize>();

    //        if (sizeManager.playerCurrentSize >= foodSize.currentSize)
    //        {
    //            sizeManager.playerCurrentSize += 0.1f;

    //            player.transform.localScale = new Vector3(sizeManager.playerCurrentSize, sizeManager.playerCurrentSize, sizeManager.playerCurrentSize);

    //            foodSize.OnDisable();
    //        }
    //        else
    //        {
    //            Debug.Log("cant eat");
    //        }
    //    }
    //}
}

