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
        Debug.Log("찍힘");
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
        SceneManager.LoadScene("Stage1"); // 게임에서 죽었을 때 재도전하는 버튼 누르면
    }

    public void GoBoss()
    {
        SoundManager.Instance.BTN_Click();
        SceneManager.LoadScene("MOB_BossScene");
    }


    public void WantedSceneEnd() // Wanted 수배지 씬 종료 후 게임 시작으로 넘어갑시다. | WantedScene - Canvas/Image에 있는 애니메이션에 해당 변수 이벤트 추가했습니다.
    {
        SceneManager.LoadScene("MOB_BossScene"); // 게임 시작 씬 넣어주십사
    }
}
