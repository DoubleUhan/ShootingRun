using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public void SampleScene()
    {
        SceneManager.LoadScene("SampleScene"); // ���� SampleScene�̾��� �� �̸� �ٲٸ� 
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
