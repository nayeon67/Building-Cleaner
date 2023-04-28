using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPosition; // 생성되는 위치
    [SerializeField] GameObject garbagePrefab;  // 쓰레기 프리팹
    
    void Start()
    {
        StartCoroutine("randSpawn");
    }

    
    void Update()
    {
        
    }

    // 위치와 스폰시간을 랜덤으로 정해서 생성
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
