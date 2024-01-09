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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                CloseClick();
            }
            else
            {
                OnClickSetting();
            }
        }
    }
    public void OnClickSetting()
    {
        settingPopUp.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void CloseClick()
    {
        isPaused = false;
        settingPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void Revenge()
    {
        SceneManager.LoadScene("SampleScene"); // 게임에서 죽었을 때 재도전하는 버튼 누르면
    }

    public void GoBoss()
    {
        SoundManager.Instance.BTN_Click();
        SceneManager.LoadScene("MOB_BossScene");
    }
}
