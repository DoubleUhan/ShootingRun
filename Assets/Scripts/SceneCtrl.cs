using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public void SceneStageScene()
    {
        SceneManager.LoadScene("StageMap"); // ���� SampleScene�̾��� �� �̸� �ٲٸ� 
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
}
