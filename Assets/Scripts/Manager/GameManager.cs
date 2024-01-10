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
    public GameObject[] cameras; // ī�޶� 


    [Header("FadeOut ���� ����")]
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
            // ���̵� �ƿ�, ĳ���� ������ �̵�, ī�޶� ����, �� �Ѿ��
            isClear = true;
            cameras[0].transform.SetParent(null);
            cameras[1].transform.SetParent(null);
            fadeBG.SetActive(true);
            StartCoroutine(SceneMoveWait(2f));
            Debug.Log("�������� Ŭ����");
        }
    }

    IEnumerator SceneMoveWait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("StageMap");
    }
}
