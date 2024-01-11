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
    private int goalEnemyCount = 3;
    public GameObject[] cameras; // ī�޶� 

    [Header("FadeOut ���� ����")]
    [SerializeField] GameObject fadeBG;
    [HideInInspector] public bool stage1_Clear;

    public GameObject gameoverPopup;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(SceneMoveWait(2));
        }
    }

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
            stage1_Clear = true;
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
        SceneManager.LoadScene("MOB_BossScene");
    }
}
