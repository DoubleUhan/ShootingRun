using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;

    //총알 프리팹
    public GameObject bulletPrefab;
    public GameObject playerEffect;
    public BossPlayCtrl player; // 메인 플레이어
    public GameObject target; // 따라볼 타겟

    Rigidbody rb;

    //발사 간격
    public float shootInterval;

    // 원으로 이동 관련
    float tempAngle = 0;
    [SerializeField]
    float angle; // 각도를 저장할 변수
    float radius = 15f; // 원의 반지름

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<BossPlayCtrl>();
        target = GameObject.FindWithTag("target");

        StartCoroutine(ShootingCor());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * Vector3.Distance(transform.position, player.transform.position) * speed;
        }
        transform.LookAt(target.transform.position);

    }

    private IEnumerator ShootingCor()
    {
        if (bulletPrefab != null)
        {
            WaitForSeconds delay = new WaitForSeconds(shootInterval);
            while (true)
            {
                GameObject playerEffectObj = Instantiate(playerEffect, player.transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity); // 총알 발사 직전 이펙트 적용
                Destroy(playerEffectObj, 0.5f);
                BulletCtrl bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity).GetComponent<BulletCtrl>();
                yield return delay;
            }
        }
    }



    //public void Spawn()
    //{
    //    int random = Random.Range(0, 3);

    //    // 클론 생성
    //    num = 3; // PlayerPrefs.GetInt("PlayerCount");
    //    for (int i = 0; i < num; i++)
    //    {
    //        Vector3 shooterSpawn = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    //        GameObject spawnShooter = Instantiate(shooter[random].gameObject, shooterSpawn, Quaternion.Euler(0f, -90f, 0f));
    //    }
    //}
}
