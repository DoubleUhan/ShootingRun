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
        if (other.CompareTag("Boss"))
        {
            BossCtrl testBoss = other.gameObject.GetComponent<BossCtrl>();
            testBoss.OnDamaged(player.ATK); // 플레이어 데미지 10으로 고정값 설정함
            Destroy(gameObject);
        }
    }

}
