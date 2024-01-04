using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletCtrl : MonoBehaviour
{
    public BossPlayCtrl player;

    void Start()
    {
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TestEnemy testEnemy = other.gameObject.GetComponent<TestEnemy>();
            testEnemy.OnDamaged(player.ATK);
        }
    }

}
