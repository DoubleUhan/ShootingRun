using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
