using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    public Transform center;
    public float radius = 2.0f;
    public float speed = 2.0f;

    private float angle = 0;

    void Update()
    {
        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
