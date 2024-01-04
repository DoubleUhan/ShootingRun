using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public GameObject main_Camera;

    public GameObject prefabToSpawn; // ������ ������

    public GameObject target; // �ٶ� Ÿ��

    private float zRange = 20; // �� �̵� ����

    // ������ �̵� ����
    float angle; // ������ ������ ����
    float radius = 20.8f; // ���� ������

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

        // ������ ������ Ÿ�� �������� �ٶ�(���ӿ�����Ʈ)
        transform.LookAt(target.transform.position);

    }
    void Move() // ���� � �̵�
    {
        float h = Input.GetAxisRaw("Horizontal");

        // ���� ������Ʈ
        angle += h * speed * Time.deltaTime;

        // ������ �̿��Ͽ� �� ���� ��ġ ���
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;


        // ĳ������ ��ġ�� ������Ʈ
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

        // �ٶ󺸴� ���� ������
        Vector3 playerDirection = transform.forward;
        // �߻�
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
