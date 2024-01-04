using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPlayCtrl : Stats
{
    [SerializeField] float speed;

    [Header("플레이어 공격 관련 변수")]
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;
    [SerializeField] float shotPower;


    [SerializeField] GameObject[] copyPlayer_Pos;

    Collider colliders;
    NavMeshAgent agent;

    public GameObject main_Camera;

    public GameObject prefabToSpawn; // 생성할 프리팹

    public GameObject target; // 바라볼 타겟

    private float zRange = 20; // 맵 이동 범위

    // 원으로 이동 관련
    float angle; // 각도를 저장할 변수
    float radius = 20.8f; // 원의 반지름

    void Awake()
    {
        colliders = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        Shot();
        Reload();
        Move();

        // 위에서 선언한 타겟 방향으로 바라봄(게임오브젝트)
        transform.LookAt(target.transform.position);

    }
    void Move() // 원형 곡선 이동
    {
        float h = Input.GetAxisRaw("Horizontal");

        // 각도 업데이트
        angle += h * speed * Time.deltaTime;

        // 각도를 이용하여 원 위의 위치 계산
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;


        // 캐릭터의 위치를 업데이트
        transform.position = new Vector3(x, transform.position.y, z);

        Vector3 vector = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vector).normalized;
    }

    void Shot()
    {
        if (curShorDelay < maxShotDelay)
            return;

        GameObject bullet = Instantiate(bullet1, transform.position, Quaternion.identity);
        BossBulletCtrl bulletCtrl = bullet.GetComponent<BossBulletCtrl>();
        bulletCtrl.player = this;

        Rigidbody rigid = bullet.GetComponent<Rigidbody>();

        // 바라보는 방향 가져옴
        Vector3 playerDirection = transform.forward;
        // 발사
        rigid.AddForce(playerDirection * shotPower, ForceMode.Impulse);

        curShorDelay = 0;

        Destroy(bullet, 5f);
    }

    void Reload()
    {
        curShorDelay += Time.deltaTime;
    }
    void Add(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject clone = Instantiate(prefabToSpawn, copyPlayer_Pos[i].transform.position, Quaternion.identity);

            if (clone.TryGetComponent<BossPlayCtrl>(out var playCtrl))
            {
                Destroy(playCtrl.colliders);
                Destroy(playCtrl.main_Camera);
            }
        }
    }
}
