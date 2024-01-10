using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemyCount;
    public const int goalEnemyCount = 100;
    public GameObject[] cameras; // 카메라 


    [Header("FadeOut 관련 변수")]
    [SerializeField] GameObject fadeBG;
    public bool isClear;
    void Awake()
    {
        if (Instance != null)
            return;
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Screen.SetResolution(540, 960, false);
        // image = GetComponent<Image>();
    }

    public void ClearStage()
    {
        if (enemyCount >= goalEnemyCount)
        {
            // 페이드 아웃, 캐릭터 앞으로 이동, 카메라 고정, 씬 넘어가기
            isClear = true;
            cameras[0].transform.SetParent(null);
            cameras[1].transform.SetParent(null);
            fadeBG.SetActive(true);
            StartCoroutine(SceneMoveWait(2f));
            Debug.Log("스테이지 클리어");
        }
    }

    IEnumerator SceneMoveWait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("StageMap");
    }
}
