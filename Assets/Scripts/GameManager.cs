using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemyCount;
    public const int goalEnemyCount = 100;
    void Awake()
    {
        Instance = this;
    }


    void ClearStage()
    {
        if (enemyCount <= goalEnemyCount)
        {
            Debug.Log("다음 스테이지로 간다잇");
        }

    }
    void Update()
    {

    }
}
