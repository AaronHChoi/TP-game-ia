using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject player;
    public float xPos;
    public float zPos;
    public int PlayerCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        xPos = Random.Range(-13, 83);
        zPos = Random.Range(32, -60);
        Instantiate(player, new Vector3(xPos, 24, zPos), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }
}
