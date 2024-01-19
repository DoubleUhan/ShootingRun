using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.Timeline;
using Unity.VisualScripting;

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

    public UnityEngine.UI.Slider playerHP_bar; // 저 뭐 플레이어 체력바

    public GameObject main_Camera;

    public GameObject warning1;
    public GameObject warning2;

    public GameObject prefabToSpawn; // 생성할 프리팹

    public GameObject[] target; // 바라볼 타겟

    public BossCtrl gameend;

    public Text playerHp_T;

    public float playerMax_HP; // 플레이어 최대 체력

    private float zRange = 20; // 맵 이동 범위

    public GameObject[] shooter; // 슈터 클론 생성할 오브젝트

    public Quaternion playerRotation;

    float num;
    public float PlayerCount; // 일반 스테이지에서 넘어온 슈터 숫자;

    public Text shooterCount;

    // 원으로 이동 관련
    float tempAngle = 0;
    [SerializeField]
    float angle; // 각도를 저장할 변수
    float radius = 15f; // 원의 반지름


    void Start()
    {
        //shooter.GetComponent<BossShooter>().Spawn();
        //PlayerPrefs.SetInt("PlayerCount", 3);
        ShooterSpawn();
        Debug.Log(PlayerPrefs.GetInt("PlayerCount"));

        //HP = PlayerPrefs.GetInt("PlayerCount");
        //Debug.Log($"player Max hp = {HP}");
        //HP *= 100;
        //playerMax_HP = HP;
        colliders = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //  Shot();
        Reload();
        Move();
        TargetLook();

        playerRotation = transform.rotation;

        shooterCount.text = PlayerCount.ToString();

        // 애들 다 죽으면 게임 종료
        if (PlayerCount <= 0)
        {
            Camera.main.transform.SetParent(null);
            gameend.GameFail();
        }
    }

    void ShooterSpawn()
    {
        //클론 생성
        num = 15; // PlayerPrefs.GetInt("PlayerCount");
        PlayerCount = num; //PlayerPrefs.GetInt("PlayerCount");
        
        
        Debug.Log(PlayerCount);

        for (int i = 0; i < num; i++)
        {
            int random = Random.Range(0, shooter.Length);
            Vector3 shooterSpawn = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Instantiate(shooter[random], shooterSpawn, Quaternion.Euler(0f, -90f, 0f));
        }
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

        if (x < 6f || x > 20f || z < -16f || z > 15f)
        {
            angle = tempAngle;
            // 움직이지 않도록 처리
            return;
        }
        tempAngle = angle;

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
    //public void Hurt(int damage)
    //{
    //    Debug.Log("데미지 받기 전" + HP);
    //    HP -= damage; // 보스 공격 데미지 다 똑같은듯
    //    Debug.Log("데미지 받고 후" + HP);

    //    playerHP_bar.value = HP / playerMax_HP;

    //    playerHp_T.text = HP.ToString() + "  /  " + playerMax_HP.ToString();

    //    if (HP <= 0)
    //    {
    //        // 보스컨트롤 함수 GameFail();
    //        gameend.GameFail();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(other.gameObject.name);
    //    if (other.gameObject.CompareTag("BossPunch"))
    //    {
    //        //플레이어까지 없애면 게임 터짐 오류오류오류오류오
    //        //Destroy(gameObject);
    //        //PlayerCount -= 1;
    //        Debug.Log("남은 아이들은 : " + PlayerCount);
    //    }
    //}

}
