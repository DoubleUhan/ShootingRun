using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletCtrl : MonoBehaviour
{
    public BossPlayCtrl player;
    public GameObject target; // ���� Ÿ��

    private GameObject playerController;

    public void GetTarget(GameObject gameObject)
    {
        target = gameObject;
        transform.LookAt(target.transform.position);
    }

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.position += transform.forward * (10f * Time.deltaTime);
        
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
