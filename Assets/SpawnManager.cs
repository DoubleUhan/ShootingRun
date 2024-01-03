using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject[] arithmetic_Object;
    int randomIndex;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(Produce());
    }
    void Arithmetic() // 사칙연산
    {

    }
    IEnumerator Produce()
    {
        while (true)
        {
            randomIndex = Random.Range(0, 4);
            GameObject randomObject = Instantiate(arithmetic_Object[randomIndex], new Vector3(-9, 1, -2.5f), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}
