using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public void SampleScene()
    {
        SceneManager.LoadScene("Stage1"); // 원래 SampleScene이었던 씬 이름 바꾸면 
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
