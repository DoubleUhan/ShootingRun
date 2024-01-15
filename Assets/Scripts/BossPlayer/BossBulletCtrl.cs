using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletCtrl : MonoBehaviour
{
    public BossPlayCtrl player;
    public GameObject target; // 따라볼 타겟

    public void GetTarget(GameObject gameObject)
    {
        target = gameObject;
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.Translate(direction * 10f * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            BossCtrl testBoss = other.gameObject.GetComponent<BossCtrl>();
            testBoss.OnDamaged(1000f); // 플레이어 데미지 10으로 고정값 설정함
            Destroy(gameObject);
        }
    }
}
