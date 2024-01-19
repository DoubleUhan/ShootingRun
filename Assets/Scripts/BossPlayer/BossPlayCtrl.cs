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

    [Header("�÷��̾� ���� ���� ����")]
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;
    [SerializeField] float shotPower;


    [SerializeField] GameObject[] copyPlayer_Pos;

    Collider colliders;
    NavMeshAgent agent;
    Rigidbody rb;
    bool looking = false;

    public UnityEngine.UI.Slider playerHP_bar; // �� �� �÷��̾� ü�¹�

    public GameObject main_Camera;

    public GameObject warning1;
    public GameObject warning2;

    public GameObject prefabToSpawn; // ������ ������

    public GameObject[] target; // �ٶ� Ÿ��

    public BossCtrl gameend;

    public Text playerHp_T;

    public float playerMax_HP; // �÷��̾� �ִ� ü��

    private float zRange = 20; // �� �̵� ����

    public GameObject[] shooter; // ���� Ŭ�� ������ ������Ʈ

    public Quaternion playerRotation;

    float num;
    public float PlayerCount; // �Ϲ� ������������ �Ѿ�� ���� ����;

    public Text shooterCount;

    // ������ �̵� ����
    float tempAngle = 0;
    [SerializeField]
    float angle; // ������ ������ ����
    float radius = 15f; // ���� ������


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

        // �ֵ� �� ������ ���� ����
        if (PlayerCount <= 0)
        {
            Camera.main.transform.SetParent(null);
            gameend.GameFail();
        }
    }

    void ShooterSpawn()
    {
        //Ŭ�� ����
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

    void Move() // ���� � �̵�
    {

        float h = Input.GetAxisRaw("Horizontal");

        angle += h * speed * Time.deltaTime;

        // ������ �̿��Ͽ� �� ���� ��ġ ���
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        if (x < 6f || x > 20f || z < -16f || z > 15f)
        {
            angle = tempAngle;
            // �������� �ʵ��� ó��
            return;
        }
        tempAngle = angle;

        // ĳ������ ��ġ�� ������Ʈ
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

        // �ٶ󺸴� ���� ������
        Vector3 playerDirection = transform.forward;
        // �߻�
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
    //    Debug.Log("������ �ޱ� ��" + HP);
    //    HP -= damage; // ���� ���� ������ �� �Ȱ�����
    //    Debug.Log("������ �ް� ��" + HP);

    //    playerHP_bar.value = HP / playerMax_HP;

    //    playerHp_T.text = HP.ToString() + "  /  " + playerMax_HP.ToString();

    //    if (HP <= 0)
    //    {
    //        // ������Ʈ�� �Լ� GameFail();
    //        gameend.GameFail();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(other.gameObject.name);
    //    if (other.gameObject.CompareTag("BossPunch"))
    //    {
    //        //�÷��̾���� ���ָ� ���� ���� ������������������
    //        //Destroy(gameObject);
    //        //PlayerCount -= 1;
    //        Debug.Log("���� ���̵��� : " + PlayerCount);
    //    }
    //}

}
