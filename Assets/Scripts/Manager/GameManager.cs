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
    [SerializeField]
    public int goalEnemyCount;
    public GameObject[] cameras; // ī�޶� 

    [Header("FadeOut ���� ����")]
    [SerializeField] GameObject fadeBG;
    [HideInInspector] public bool stage_Clear;

    public GameObject gameoverPopup;
    public GameObject gameClearPopup;


    [Header("�÷��̾� �� ǥ�� ����")]
    public Text player_Count_T;
    public int player_Count;

    public int stage_num;

    private void Update()
    {
    }

    void Awake()
    {
        if (Instance != null)
            return;
        else
        {
            Instance = this;
        }

        Screen.SetResolution(540, 960, false);
        // image = GetComponent<Image>();
    }

    public void ClearStage()
    {
        // ���̵� �ƿ�, ĳ���� ������ �̵�, ī�޶� ����, �� �Ѿ��
        cameras[0].transform.SetParent(null);
        cameras[1].transform.SetParent(null);
        fadeBG.SetActive(true);
        StageManager.Instance.clearStageMax = PlayerPrefs.GetInt("Stage");
        StageManager.Instance.isClear_Stage[PlayerPrefs.GetInt("Stage") - 1] = true;
        PlayerPrefs.SetInt("IsClear", 1);

    }

    public void ClearStage2()
    {
        // ���̵� �ƿ�, ĳ���� ������ �̵�, ī�޶� ����, �� �Ѿ��
        cameras[0].transform.SetParent(null);
        cameras[1].transform.SetParent(null);
        fadeBG.SetActive(true);
        StageManager.Instance.clearStageMax = PlayerPrefs.GetInt("Stage");
        StageManager.Instance.isClear_Stage[PlayerPrefs.GetInt("Stage") - 1] = true;
        PlayerPrefs.SetInt("IsClear", 1);

        gameClearPopup.SetActive(true);
        Debug.Log("�������� Ŭ����");
        StartCoroutine(SceneMoveWait(2f, "MOB_BossScene"));
    }

    IEnumerator SceneMoveWait(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
    public void ClickClearBtn()
    {
        StartCoroutine(SceneMoveWait(0f, "StageMap"));
    }

    public void ClickScene1_GameOverBtn()
    {
        StartCoroutine(SceneMoveWait(0f, "Stage1"));
    }
    public void ClickScene2_GameOverBtn()
    {
        StartCoroutine(SceneMoveWait(0f, "Stage2"));
    }
}
