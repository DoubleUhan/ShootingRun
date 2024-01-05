using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemyCount;
    public const int goalEnemyCount = 100;
    public GameObject basic_camera;
    public GameObject Arithmetic_Camera;

    bool isGhostCamera;
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
    void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Arithmetic_Camera 실행");
            basic_camera.SetActive(false);
            Arithmetic_Camera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("basic_camera 실행");
            basic_camera.SetActive(true);
            Arithmetic_Camera.SetActive(false);
        }
    }
    void Update()
    {
        ChangeCamera();
    }
}
