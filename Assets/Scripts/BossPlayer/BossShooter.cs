using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossShooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;

    //총알 프리팹
    public GameObject bulletPrefab;
    public BossPlayCtrl player; // 메인 플레이어
    public BossCtrl gameend; // BossCtrl 참조?

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
        // 복사본이 움직임
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
            float shotDelay = Random.Range(0.3f, 0.6f);
            WaitForSeconds delay = new WaitForSeconds(shotDelay);
            while (true)
            {
                BossBulletCtrl bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity).GetComponent<BossBulletCtrl>();
                bullet.GetTarget(target);
                yield return delay;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("BossPunch"))
        {
            Debug.Log("슈터 맞음");
            Destroy(gameObject);
            player.PlayerCount -= 1;
            Debug.Log("남은 아이들은 : " + player.PlayerCount);

            //// 애들 다 죽으면 게임 종료
            //if (player.PlayerCount <= 0)
            //{
            //    Camera.main.transform.SetParent(null);
            //    gameend.GameFail();
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

