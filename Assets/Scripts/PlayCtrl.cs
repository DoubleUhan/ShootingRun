using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayCtrl : Stats
{
    public GameObject shooterPrefab;
    public List<Shooter> shooterList = new List<Shooter>();
    [SerializeField] float speed;

    [Header("플레이어 공격 관련 변수")]
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;


    [SerializeField] GameObject[] copyPlayer_Pos;

    Collider colliders;
    NavMeshAgent agent;

    public GameObject main_Camera;

    public GameObject prefabToSpawn; // 생성할 프리팹
    private Vector3 dir;

    void Awake()
    {
        colliders = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        //Shot();
        //Reload();
        Move();
        //Follow();
    }



    void Move()
    {
        //float h = 0, v = 0;

        //h = Input.GetAxisRaw("Horizontal");

        //var curPos = transform.position;
        //curPos += new Vector3(0, 0, h) * speed * Time.deltaTime;
        //curPos.z = Mathf.Clamp(curPos.z, -2.6f, 2.6f);

        //transform.position = curPos;
        dir.z = Mathf.Clamp(dir.z + Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, -2.6f, 2.6f);
        transform.position = dir;

    }
    //void Follow() // 원형을 따라가도록 구현
    //{
    //    agent.destination = playerFollow.position;
    //    // 길찾기 시작
    //    agent.isStopped = false;
    //}

    //void Shot()
    //{
    //    if (curShorDelay < maxShotDelay)
    //        return;

    //    GameObject bullet = Instantiate(bullet1, transform.position, transform.rotation);
    //    BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();
    //    bulletCtrl.player = this;

    //    Rigidbody rigid = bullet.GetComponent<Rigidbody>();
    //    rigid.AddForce(Vector3.left * 10, ForceMode.Impulse);
    //    curShorDelay = 0;

    //    Destroy(bullet, 5f);
    //}

    //void Reload()
    //{
    //    curShorDelay += Time.deltaTime;
    //}
    [ContextMenu("테스트")]
    public void Test()
    {
        Add(1);
    }

    public void Add(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), 1, Random.Range(-0.1f, 0.1f));
            Shooter clone = Instantiate(shooterPrefab, transform.position + randomPos, Quaternion.identity).GetComponent<Shooter>();
            clone.player = this;
            clone.transform.SetParent(clone.player.transform);
            shooterList.Add(clone);
        }
    }
    public void Sub(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (shooterList.Count > 0)
            {
                // 리스트에서 가장 최근에 생성된 Shooter를 가져오고, 해당 Shooter를 파괴하고 리스트에서 제거
                Shooter shooterToRemove = shooterList[shooterList.Count - 1];
                shooterList.RemoveAt(shooterList.Count - 1);
                Destroy(shooterToRemove.gameObject);
            }
        }
    }

    public void Mult()
    {
        int currentShooterCount = shooterList.Count;
        int totalShooterCount = currentShooterCount * 2; // 현재의 복제된 수의 2배

        for (int i = 0; i < totalShooterCount; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), 1, Random.Range(-0.1f, 0.1f));
            Shooter clone = Instantiate(shooterPrefab, transform.position + randomPos, Quaternion.identity).GetComponent<Shooter>();
            clone.player = this;
            clone.transform.SetParent(clone.player.transform);
            shooterList.Add(clone);
        }
    }
    public void Div()
    {
        int currentShooterCount = shooterList.Count;
        int totalShooterCount = Mathf.Max(1, currentShooterCount / 2); // 최소 1개 이상의 Shooter가 생성되도록 보장

        for (int i = 0; i < totalShooterCount; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), 1, Random.Range(-0.1f, 0.1f));
            Shooter clone = Instantiate(shooterPrefab, transform.position + randomPos, Quaternion.identity).GetComponent<Shooter>();
            clone.player = this;
            clone.transform.SetParent(clone.player.transform);
            shooterList.Add(clone);
        }
    }


    //void ClonePlayers()
    //{
    //    // 왼쪽에 플레이어 복제
    //    GameObject leftClonedPlayer = InstantiatePlayer(transform.position - new Vector3(cloneOffset, 0f, 0f));

    //    // 오른쪽에 플레이어 복제
    //    GameObject rightClonedPlayer = InstantiatePlayer(transform.position + new Vector3(cloneOffset, 0f, 0f));
    //}

    //GameObject InstantiatePlayer(Vector3 position)
    //{
    //    // 플레이어 오브젝트를 주어진 위치에 복제
    //    GameObject clonedPlayer = Instantiate(playerPrefab, position, Quaternion.identity);

    //    return clonedPlayer;
    //}
}