using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : Stats
{
    ParticleSystem deathParticle;
    NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayCtrl>().transform;
        deathParticle = GetComponentInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Walk();
    }
    void Walk()
    {
        agent.destination = player.position;
        // ±æÃ£±â ½ÃÀÛ
        agent.isStopped = false;
    }
   /* private void OnDestroy()
    {
        
        Destroy(this);
    }*/
    public void OnDamaged(float Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
            deathParticle.transform.parent = null;
            deathParticle.Play();
            Destroy(gameObject);
            GameManager.Instance.enemyCount++;
            if (GameManager.Instance.enemyCount >= GameManager.Instance.goalEnemyCount)
                GameManager.Instance.ClearStage();
        }
    }
}
