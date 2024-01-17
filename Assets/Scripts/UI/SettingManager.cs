using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    [SerializeField] GameObject settingPopUp;
    bool isPaused = false;

    void Awake()
    {
        Time.timeScale = 1.0f;

    }
    private void Update()
    {
    }
    public void OnClickSetting()
    {
        settingPopUp.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseClick()
    {
        settingPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        Debug.Log("����");
        SceneManager.LoadScene("StageMap");
        Time.timeScale = 1;
    }

    public void RetryBoss()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MOB_BossScene");
    }
    public void Revenge()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage1"); // ���ӿ��� �׾��� �� �絵���ϴ� ��ư ������
    }

    public void GoBoss()
    {
        SoundManager.Instance.BTN_Click();
        SceneManager.LoadScene("MOB_BossScene");
    }


    public void WantedSceneEnd() // Wanted ������ �� ���� �� ���� �������� �Ѿ�ô�. | WantedScene - Canvas/Image�� �ִ� �ִϸ��̼ǿ� �ش� ���� �̺�Ʈ �߰��߽��ϴ�.
    {
        SceneManager.LoadScene("MOB_BossScene"); // ���� ���� �� �־��ֽʻ�
    }
}
