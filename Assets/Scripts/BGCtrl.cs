using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    Renderer rend;
    readonly float scrollSpeed = 0.5f;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        rend.material.mainTextureOffset =
            new Vector2(0, -Time.deltaTime * scrollSpeed);
    }
}
