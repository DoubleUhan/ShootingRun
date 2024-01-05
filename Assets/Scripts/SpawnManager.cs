using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy_Object;
    [SerializeField] GameObject arithmetic_Object;

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
        StartCoroutine(Enemy_Produce());
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
            float arithmeticSpawnPosz = Random.Range(-2.5f, 2.5f);
            arithmeticSpawnDelay = Random.Range(arithmeticSpawnMin, arithmeticSpawnMax + 1);
            GameObject randomObject1 = Instantiate(arithmetic_Object, new Vector3(-90, 1, arithmeticSpawnPosz), Quaternion.identity);
            yield return new WaitForSeconds(arithmeticSpawnDelay);
        }
    }
}
