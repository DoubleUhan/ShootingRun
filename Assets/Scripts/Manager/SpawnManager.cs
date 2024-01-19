using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //public static SpawnManager instance;

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

    void Awake()
    {
        //if (instance != null)
        //    return;
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
    }
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
            float enemySpawnPosz = Random.Range(-4f, 4f);
            int enemyRadom = Random.Range(0, enemy_Object.Length);
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
            var randomObject = Instantiate(arithmetic_Object, new Vector3(-60f, 0.5f, 3f), Quaternion.Euler(0, 90f, 0)).GetComponent<Arithmetic>();

            randomObject.type = (ArithmeticType)spawn;

            switch (randomObject.type)
            {
                case ArithmeticType.add:
                    randomObject.value = Random.Range(3, 11);
                    randomObject.sigh_T.text = "+";
                    break;
                case ArithmeticType.sub:
                    randomObject.value = Random.Range(3, 11);
                    randomObject.sigh_T.text = "-";
                    break;
                case ArithmeticType.mult:
                    randomObject.sigh_T.text = "X";
                    randomObject.value = Random.Range(2, 4);
                    break;
                case ArithmeticType.div:
                    randomObject.sigh_T.text = "÷";
                    randomObject.value = Random.Range(2, 4);
                    break;
            }
            randomObject.value_T.text = randomObject.value.ToString();

            var randomObject2 = Instantiate(arithmetic_Object, new Vector3(-60f, 0.5f, -3f), Quaternion.Euler(0, 90f, 0)).GetComponent<Arithmetic>();
            switch (randomObject2.type)
            {
                case ArithmeticType.add:
                    randomObject2.value = Random.Range(3, 11);
                    randomObject2.sigh_T.text = "+";
                    break;
                case ArithmeticType.sub:
                    randomObject2.value = Random.Range(3, 11);
                    randomObject2.sigh_T.text = "-";
                    break;
                case ArithmeticType.mult:
                    randomObject2.value = Random.Range(2, 4);
                    randomObject2.sigh_T.text = "X";
                    break;
                case ArithmeticType.div:
                    randomObject2.value = Random.Range(2, 4);
                    randomObject2.sigh_T.text = "÷";
                    break;
            }
            randomObject2.value_T.text = /*randomObject2.sigh_T.text +*/ randomObject2.value.ToString();

            randomObject.Pair = randomObject2;
            randomObject2.Pair = randomObject;

            yield return new WaitForSeconds(arithmeticSpawnDelay);
        }
    }

    IEnumerator Needle_Produce()
    {
        while (true)
        {
            float randomValue = (Random.Range(0, 2) * 5) - 2.5f;
            needleSpawnDelay = Random.Range(needleSpawnMin, needleSpawnMax + 1);
            GameObject needle = Instantiate(needle_Object, new Vector3(-90, 2.3f, randomValue), Quaternion.identity);
            yield return new WaitForSeconds(needleSpawnDelay);

        }
    }
}
