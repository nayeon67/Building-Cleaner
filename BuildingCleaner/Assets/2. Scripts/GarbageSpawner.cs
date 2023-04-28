using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] GameObject garbagePrefab;
    
    void Start()
    {
        StartCoroutine("randSpawn");
    }

    
    void Update()
    {
        
    }

    IEnumerator randSpawn()
    {
        while(true)
        {
            int positionNum = Random.Range(0, 4);
            Instantiate(garbagePrefab, spawnPosition[positionNum]);
            float spawnTime = Random.Range(5f, 7f);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
