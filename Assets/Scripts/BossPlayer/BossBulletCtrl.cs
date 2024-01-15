using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletCtrl : MonoBehaviour
{
    public BossPlayCtrl player;
    public GameObject target; // ���� Ÿ��

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
            testBoss.OnDamaged(1000f); // �÷��̾� ������ 10���� ������ ������
            Destroy(gameObject);
        }
    }
}
