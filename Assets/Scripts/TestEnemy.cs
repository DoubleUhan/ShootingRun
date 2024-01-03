using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        player = FindObjectOfType<PlayCtrl>().transform;
    }
    void Update()
    {
        Walk();
        Debug.Log(player.transform.position);
    }
    void Walk()
    {
        agent.destination = player.position;
        // 길찾기 시작
        agent.isStopped = false;
    }
}
