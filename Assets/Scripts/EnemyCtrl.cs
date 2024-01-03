using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : Stats
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
        // 현재 위치를 가져오고 x축으로 이동
        Vector3 newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // 새로운 위치로 이동
        transform.position = newPosition;
    }
}
