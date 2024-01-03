using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public float bg_Speed;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.time * bg_Speed, 0));
    }
}
