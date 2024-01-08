using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] Text stageNum_T;
    [SerializeField] GameObject[] stage_Btn;

    public GameObject stagePopup;

    private void Awake()
    {
        instance = this;
    }

    public void ArriveStage(int stageNum)
    {
        Debug.Log("ArriceStage");
        stagePopup.SetActive(true);
        stageNum_T.text = "Stage" + stageNum;

        stage_Btn[stageNum - 1].SetActive(true);
    }
}
