using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.Timeline;

public class BossPlayCtrl : Stats
{
    [SerializeField] float speed;

    [Header("�÷��̾� ���� ���� ����")]
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;
    [SerializeField] float shotPower;
    [SerializeField] float playerHP;


    [SerializeField] GameObject[] copyPlayer_Pos;

    Collider colliders;
    NavMeshAgent agent;
    Rigidbody rb;
    bool looking = false;

    public UnityEngine.UI.Slider playerHP_bar; // �� �� �÷��̾� ü�¹�

    public GameObject main_Camera;

    public GameObject prefabToSpawn; // ������ ������

    public GameObject[] target; // �ٶ� Ÿ��

    public float player_HP; // �÷��̾� ���� ü��
    public float playerMax_HP; // �÷��̾� �ִ� ü��

    private float zRange = 20; // �� �̵� ����

    // ������ �̵� ����
    float tempAngle = 0;
    [SerializeField]
    float angle; // ������ ������ ����
    float radius = 30f; // ���� ������

    void Awake()
    {
        player_HP = PlayerPrefs.GetInt("PlayerCount");
        Debug.Log($"player hp = {player_HP}");
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

    void Move() // ���� � �̵�
    {

        float h = Input.GetAxisRaw("Horizontal");

        angle += h * speed * Time.deltaTime;

        // ������ �̿��Ͽ� �� ���� ��ġ ���
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        if (x < 1f || x > 30f || z < -30f || z > 30f)
        {
            angle = tempAngle;
            // �������� �ʵ��� ó��
            return;
        }
        tempAngle = angle;

        // x ��ǥ�� -4���� 20 ���̷�, z ��ǥ�� -20���� 20 ���̷� ����


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

    public void Hurt()
    {
        playerHP -= 2; // �ϴ� ��� ���� ������ �����ϰ�..

        if (playerHP <= 0)
        {
            // ������Ʈ�� �Լ� GameFail();
        }

        playerHP_bar.value = player_HP / playerMax_HP;
    }

    //�÷��̾� ü�¹�
    //playerHP_bar.value = player_HP / playerMax_HP;

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("explode"))
    //    {
    //        Destroy(gameObject);
    //        BossCtrl.GameFail();
    //    }
    //}
}
