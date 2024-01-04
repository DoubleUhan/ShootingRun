using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] GameObject enemy_Object;
    [SerializeField] GameObject arithmetic_Object;
    int randomIndex;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //   StartCoroutine(Arithmetic_Produce());
        StartCoroutine(Enemy_Produce());
        StartCoroutine(Arithmetic_Produce());
    }
    void Arithmetic() // 사칙연산
    {

    }

    IEnumerator Enemy_Produce()
    {
        while (true)
        {
            GameObject Enemy1 = Instantiate(enemy_Object, new Vector3(-9, 1, -2.5f), Quaternion.identity);
            GameObject Enemy2 = Instantiate(enemy_Object, new Vector3(-9, 1, 2.5f), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);

        }
    }

    IEnumerator Arithmetic_Produce()
    {
        while (true)
        {
            GameObject randomObject1 = Instantiate(arithmetic_Object, new Vector3(-9, 1, -2.5f), Quaternion.identity);
            GameObject randomObject2 = Instantiate(arithmetic_Object, new Vector3(-9, 1, 2.5f), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}
