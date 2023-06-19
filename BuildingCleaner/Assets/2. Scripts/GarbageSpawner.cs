using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions; // 생성되는 위치
    [SerializeField] GameObject garbagePrefab;  // 쓰레기 프리팹
    private Vector3 spawnPosition;
    
    void Start()
    {
        StartCoroutine("randSpawn");
    }

    
    // 위치와 스폰시간을 랜덤으로 정해서 생성
    IEnumerator randSpawn()
    {
        while(GameManager.Instance.isGameTime)
        {
            int positionNum = Random.Range(0, 4);
            spawnPosition = spawnPositions[positionNum].position;
            Instantiate(garbagePrefab, spawnPosition, Quaternion.identity);
            float spawnTime = Random.Range(5f, 7f);
            SoundManager.Instance.PlaySFXSound("FallingDown");
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
