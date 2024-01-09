using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemy_Object;
    [SerializeField] GameObject[] arithmetic_Object;
    [SerializeField] GameObject needle_Object;

    [Header("Enemy 생성 주기를 위한 변수")]
    [SerializeField] float enemySpawnMin;
    [SerializeField] float enemySpawnMax;
    float enemySpawnDelay;

    [Header("Arithmetic 생성 주기를 위한 변수")]
    [SerializeField] float arithmeticSpawnMin;
    [SerializeField] float arithmeticSpawnMax;
    float arithmeticSpawnDelay;

    [Header("Needle 생성 주기를 위한 변수")]
    [SerializeField] float needleSpawnMin;
    [SerializeField] float needleSpawnMax;
    float needleSpawnDelay;

    void Start()
    {
        StartCoroutine(Enemy_Produce());
        StartCoroutine(Arithmetic_Produce());
        StartCoroutine(Needle_Produce());
    }
    IEnumerator Enemy_Produce()
    {
        while (true)
        {
            float enemySpawnPosz = Random.Range(-0.7f, 0.7f);
            int enemyRadom = Random.Range(0, 2);
            Debug.Log(enemyRadom);
            enemySpawnDelay = Random.Range(enemySpawnMin, enemySpawnMax + 1);
            GameObject Enemy = Instantiate(enemy_Object[enemyRadom], new Vector3(-60f, 0.5f, enemySpawnPosz), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
    IEnumerator Arithmetic_Produce()
    {
        while (true)
        {
            int spawn1 = Random.Range(0, 8);
            int spawn2 = Random.Range(0, 8);
            arithmeticSpawnDelay = Random.Range(arithmeticSpawnMin, arithmeticSpawnMax + 1);
            GameObject randomObject1 = Instantiate(arithmetic_Object[spawn1], new Vector3(-60f, 1, -0.7f), Quaternion.identity);
            GameObject randomObject2 = Instantiate(arithmetic_Object[spawn2], new Vector3(-60f, 1, 0.7f), Quaternion.identity);
            yield return new WaitForSeconds(arithmeticSpawnDelay);
        }
    }

    IEnumerator Needle_Produce()
    {
        while (true)
        {
            Debug.Log("니들 생성");
            float randomValue = (Random.Range(0, 2) * 5) - 2.5f;
            needleSpawnDelay = Random.Range(needleSpawnMin, needleSpawnMax + 1);
            GameObject needle = Instantiate(needle_Object, new Vector3(-90, 1, randomValue), Quaternion.identity);
            yield return new WaitForSeconds(needleSpawnDelay);

        }
    }
}
