using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Color color = image.color;

        if (color.a < 1)
        {
            color.a += Time.deltaTime;
        }
        image.color = color;


        //if (image.color.a >= 255 && GameManager.Instance.stage_num == 1)
        //{
        //    Debug.Log("123123");
        //    GameManager.Instance.gameClearPopup.SetActive(true);
        //    Time.timeScale = 0;
        //}
    }
}
