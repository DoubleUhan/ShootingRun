using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosXMove : MonoBehaviour
{
    public float moveSpeed = 5;
    void Update()
    {
        Move();
    }

    void Move()
    {
        // 현재 위치를 가져오고 x축으로 이동
        Vector3 newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // 새로운 위치로 이동
        transform.position = newPosition;
    }

}
