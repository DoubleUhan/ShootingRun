using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BossBulletCtrl : MonoBehaviour
{
    public BossPlayCtrl player;
    public GameObject target; // 따라볼 타겟
    public ParticleSystem attackParticle;

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
            StartCoroutine(BulletDelete());
            testBoss.OnDamaged(10f); // 플레이어 데미지 10으로 고정값 설정함

        }
    }

    IEnumerator BulletDelete()
    {

        yield return new WaitForSeconds(.3f);

        // 보스에게 닿았을때 파티클 실행
        attackParticle.transform.parent = null;
        attackParticle.Play();

        Destroy(gameObject);
    }
}
