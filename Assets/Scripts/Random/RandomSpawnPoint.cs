using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnPoint : MonoBehaviour
{
    public DataBase dataBase;
    Dictionary<Transform, float> _items;
    public List<RarityInfo> rarityInfo;
    private void Awake()
    {
        //_items = new Dictionary<Transform, float>();
        //for (int i = 0; i < rarityInfo.Count; i++)
        //{
        //    var curr = rarityInfo[i];
        //    _items[curr.rarity] = curr.tr;
        //}
    }
    public void GetRandomSpawnpoint()
    {
        var rarity = Random.Roulette(_items);
        SetRandompoint(rarity);
    }
    void SetRandompoint(Transform transform)
    {
        if (!dataBase) return;
        if (!dataBase.cards.ContainsKey(transform)) return;
    }
}