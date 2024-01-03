using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [SerializeField] GameObject settingPopUp;

    private void Start()
    {
        
    }
    void Awake()
    {
        Time.timeScale = 1.0f;

    }
    private void Update()
    {
        Debug.Log(Time.timeScale);
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
}
