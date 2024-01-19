using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosXMove : MonoBehaviour
{
    public float moveSpeed;
    void Update()
    {
        Move();
    }

    void Move()
    {
        // ���� ��ġ�� �������� x������ �̵�
        Vector3 newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // ���ο� ��ġ�� �̵�
        transform.position = newPosition;
    }

}
