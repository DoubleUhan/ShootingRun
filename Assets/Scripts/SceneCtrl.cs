using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public void SampleScene()
    {
        SceneManager.LoadScene("Stage1"); // ���� SampleScene�̾��� �� �̸� �ٲٸ� 
    }
    public void Home()
    {
        SceneManager.LoadScene("StageMap");
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
