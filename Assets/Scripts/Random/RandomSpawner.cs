using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] SpawnPoints;
    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        yield return null;

        if (player != null)
        {
            Dictionary<GameObject, float> spawnWeights = new Dictionary<GameObject, float>();
            float defaultWeight = 1f / SpawnPoints.Length;

            foreach (GameObject SpawnPoint in SpawnPoints)
                spawnWeights.Add(SpawnPoint, defaultWeight);

            GameObject selectedSpawnPoint = MyRandoms.Roulette(spawnWeights);

            Vector3 spawnPosition = selectedSpawnPoint.transform.position;
            player.transform.position = new Vector3(spawnPosition.x, 168.2f, spawnPosition.z);
        }
    }
}
