using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(540, 960, false);
    }
    public void SceneStageScene()
    {
        SceneManager.LoadScene("StageMap"); // ���� SampleScene�̾��� �� �̸� �ٲٸ� 
    }
    public void BossStageBtn()
    {
        StageManager.Instance.clearStageMax = Mathf.Clamp(StageManager.Instance.clearStageMax - 1, 0, 2);
        StageManager.Instance.SetNextStagePos();
        //  Destroy(StageManager.Instance.stageCol);
        SceneManager.LoadScene("StageMap"); // ���� SampleScene�̾��� �� �̸� �ٲٸ� 

        //StageManager.Instance.SetNextStagePos();
    }
    public void Stage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void OnClickStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void OnClickStage2()
    {
        SceneManager.LoadScene("StageMap");
    }
    public void OnClickBossStage()
    {
        SceneManager.LoadScene("MOB_BossScene");
    }

    public void EndBoss()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("home");
    }
}
