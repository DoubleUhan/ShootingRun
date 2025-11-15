using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedSceneManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(LoadingAnimation());
    }

    IEnumerator LoadingAnimation() {
        yield return new WaitForSeconds(4f);

        SettingManager settingManager = GetComponent<SettingManager>();
        settingManager.WantedSceneEnd();
    }
}
