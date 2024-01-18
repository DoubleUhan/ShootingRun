using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public bool isEnd;
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
            isEnd = false;
            color.a += Time.deltaTime;
        }
        else
            isEnd = true;
        image.color = color;
    }
}
