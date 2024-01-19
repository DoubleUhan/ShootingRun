using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemyCount;
    [SerializeField]
    public int goalEnemyCount;
    public GameObject[] cameras; // ī�޶� 

    [Header("FadeOut ���� ����")]
    [SerializeField] FadeOut fadeBG;
    [HideInInspector] public bool stage_Clear;

    public GameObject gameoverPopup;
    public GameObject gameClearPopup;


    [Header("�÷��̾� �� ǥ�� ����")]
    public Text player_Count_T;
    public int player_Count;

    public int stage_num;

    public bool gameEnd;


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
        gameEnd = true;
        // ���̵� �ƿ�, ĳ���� ������ �̵�, ī�޶� ����, �� �Ѿ��
        cameras[0].transform.SetParent(null);
        cameras[1].transform.SetParent(null);
        fadeBG.gameObject.SetActive(true);
        StageManager.Instance.clearStageMax = PlayerPrefs.GetInt("Stage");
        StageManager.Instance.isClear_Stage[PlayerPrefs.GetInt("Stage") - 1] = true;
        PlayerPrefs.SetInt("IsClear", 1);

        StartCoroutine(GameEndPopup(gameClearPopup));
        //gameClearPopup.SetActive(true);
    }

    public void OverStage()
    {
        gameEnd = true;
        fadeBG.gameObject.SetActive(true);
        StartCoroutine(GameEndPopup(gameoverPopup));
    }

    private IEnumerator GameEndPopup(GameObject popup)
    {
        while (!fadeBG.isEnd)
        {
            yield return null;
        }
        fadeBG.gameObject.SetActive(false);
        popup.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClickBtn(string sceneName)
    {
        Debug.Log("Ŭ��Ŭ��");
        StartCoroutine(SceneMoveWait(0f, sceneName));
    }

    IEnumerator SceneMoveWait(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
