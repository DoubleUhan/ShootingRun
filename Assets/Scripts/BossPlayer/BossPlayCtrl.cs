using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    Rigidbody rb;
    bool looking = false;

    public GameObject main_Camera;

    public GameObject prefabToSpawn; // 생성할 프리팹

    public GameObject[] target; // 바라볼 타겟

    private float zRange = 20; // 맵 이동 범위

    // 원으로 이동 관련
    float tempAngle = 0;
    [SerializeField]
    float angle; // 각도를 저장할 변수
    float radius = 30f; // 원의 반지름

    void Awake()
    {
        colliders = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Shot();
        Reload();
        Move();
        TargetLook();
    }

    void TargetLook()
    {
        transform.LookAt(target[0].transform.position);
    }

    void TargetLook1()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int random = Random.Range(1, 4);
            transform.LookAt(target[random].transform.position);
            Debug.Log(target[random]);
            StartCoroutine(Wait(5));
        }
        else
        {
            transform.LookAt(target[0].transform.position);
        }
    }

    IEnumerator Wait(float time)
    {
        looking = true;
        yield return new WaitForSeconds(time);
        looking = false;
    }

    void Move() // 원형 곡선 이동
    {

        float h = Input.GetAxisRaw("Horizontal");

        angle += h * speed * Time.deltaTime;

        // 각도를 이용하여 원 위의 위치 계산
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        if (x < 1f || x > 30f || z < -30f || z > 30f)
        {
            angle = tempAngle;
            // 움직이지 않도록 처리
            return;
        }
        tempAngle = angle;

        // x 좌표를 -4에서 20 사이로, z 좌표를 -20에서 20 사이로 제한


        // 캐릭터의 위치를 업데이트
        transform.position = new Vector3(x, transform.position.y, z);

        Vector3 vector = target[0].transform.position - transform.position;
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
        SoundManager.Instance.Boss_PlayerShot();
        curShorDelay = 0;
    }

    void Reload()
    {
        curShorDelay += Time.deltaTime;
    }

    public void OnDamege(float damege)
    {
        HP -= damege;
    }

    void Add(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject clone = Instantiate(prefabToSpawn, copyPlayer_Pos[i].transform.position, Quaternion.identity);

            if (clone.TryGetComponent<BossPlayCtrl>(out var playCtrl))
            {
                playCtrl.main_Camera.transform.SetParent(null);
                Destroy(playCtrl.colliders);

            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("explode"))
    //    {
    //        Destroy(gameObject);
    //        BossCtrl.GameFail();
    //    }
    //}
}
