using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform target;

    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        nav.SetDestination(target.position);
    }

    void Move()
    {
        // ���� ��ġ�� �������� x������ �̵�
        Vector3 newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // ���ο� ��ġ�� �̵�
        transform.position = newPosition;
    }
}
