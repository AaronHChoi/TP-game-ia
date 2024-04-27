using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public Dictionary<Transform, float> cards = new Dictionary<Transform, float>();

    public List<Transform> spawnPoint = new List<Transform>();
    private void Awake()
    {
        //FindCards
        var ur = Resources.LoadAll<Sprite>("Cards/UR");
        var sr = Resources.LoadAll<Sprite>("Cards/SR");
        var r = Resources.LoadAll<Sprite>("Cards/R");
        var c = Resources.LoadAll<Sprite>("Cards/C");

        for (int i = 0; i < spawnPoint.Count; i++)
        {
            cards.Add(spawnPoint[i], i);
        }

        ////AddCardsToDataBase
        //cards.Add(RarirtyEnum.UR, ur);
        //cards.Add(RarirtyEnum.SR, sr);
        //cards.Add(RarirtyEnum.R, r);
        //cards.Add(RarirtyEnum.C, c);


    }

   
}
