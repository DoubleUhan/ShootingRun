using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        // ���� ��ġ�� �������� x������ �̵�
        Vector3 newPosition = transform.position + direction * Time.deltaTime * speed;
        // ���ο� ��ġ�� �̵�
        transform.position = newPosition;
    }
}
