using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemy_Object;
    [SerializeField] GameObject arithmetic_Object;
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
            int spawn = Random.Range(0, 4); // +,-,*,/가 랜덤으로 생성

            arithmeticSpawnDelay = Random.Range(arithmeticSpawnMin, arithmeticSpawnMax + 1);
            var randomObject = Instantiate(arithmetic_Object, new Vector3(-60f, 1, 4f), Quaternion.identity).GetComponent<Arithmetic>();
            randomObject.type = (ArithmeticType)spawn;

            switch (randomObject.type)
            {
                case ArithmeticType.add:
                    randomObject.value = Random.Range(3, 11);
                    randomObject.value_T.text = "+" + randomObject.value;
                    break;
                case ArithmeticType.sub:
                    randomObject.value = Random.Range(3, 11);
                    randomObject.value_T.text = "-" + randomObject.value;
                    break;
                case ArithmeticType.mult:
                    randomObject.value = Random.Range(2, 4);
                    randomObject.value_T.text = "X" + randomObject.value;
                    break;
                case ArithmeticType.div:
                    randomObject.value = Random.Range(2, 4);
                    randomObject.value_T.text = "÷" + randomObject.value;
                    break;
            }
            var randomObject2 = Instantiate(arithmetic_Object, new Vector3(-60f, 1, -4f), Quaternion.identity).GetComponent<Arithmetic>();
            switch (randomObject2.type)
            {
                case ArithmeticType.add:
                    randomObject2.value = Random.Range(3, 11);
                    randomObject2.value_T.text = "+" + randomObject2.value;
                    break;
                case ArithmeticType.sub:
                    randomObject2.value = Random.Range(3, 11);
                    randomObject2.value_T.text = "-" + randomObject2.value;
                    break;
                case ArithmeticType.mult:
                    randomObject2.value = Random.Range(2, 4);
                    randomObject2.value_T.text = "X" + randomObject2.value;
                    break;
                case ArithmeticType.div:
                    randomObject2.value = Random.Range(2, 4);
                    randomObject2.value_T.text = "÷" + randomObject2.value;
                    break;
            }
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
