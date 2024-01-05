using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy_Object;
    [SerializeField] GameObject[] arithmetic_Object;

    [Header("Enemy 생성 주기를 위한 변수")]
    [SerializeField] float enemySpawnMin;
    [SerializeField] float enemySpawnMax;
    float enemySpawnDelay;

    [Header("Arithmetic 생성 주기를 위한 변수")]
    [SerializeField] float arithmeticSpawnMin;
    [SerializeField] float arithmeticSpawnMax;
    float arithmeticSpawnDelay;

    void Start()
    {
        //    StartCoroutine(Enemy_Produce());
        StartCoroutine(Arithmetic_Produce());
    }
    IEnumerator Enemy_Produce()
    {
        while (true)
        {
            float enemySpawnPosz = Random.Range(-2.5f, 2.5f);
            enemySpawnDelay = Random.Range(enemySpawnMin, enemySpawnMax + 1);
            GameObject Enemy = Instantiate(enemy_Object, new Vector3(-90, 1, enemySpawnPosz), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnDelay);

        }
    }
    IEnumerator Arithmetic_Produce()
    {
        while (true)
        {
            int spawn1 = Random.Range(0, 8);
            int spawn2 = Random.Range(0, 8);
            Debug.Log("spawn1:" + spawn1 + "spawn2:" + spawn2);
            arithmeticSpawnDelay = Random.Range(arithmeticSpawnMin, arithmeticSpawnMax + 1);
            GameObject randomObject1 = Instantiate(arithmetic_Object[spawn1], new Vector3(-90, 1, -2.5f), Quaternion.identity);
            GameObject randomObject2 = Instantiate(arithmetic_Object[spawn2], new Vector3(-90, 1, 2.5f), Quaternion.identity);
            yield return new WaitForSeconds(arithmeticSpawnDelay);
        }
    }
}
