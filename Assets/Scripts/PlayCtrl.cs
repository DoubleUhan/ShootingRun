using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayCtrl : Stats
{
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
        //Follow();
    }



    void Move()
    {
        float h = 0, v = 0;

        h = Input.GetAxisRaw("Horizontal");

        var curPos = transform.position;
        curPos += new Vector3(0, 0, h) * speed * Time.deltaTime;
        curPos.z = Mathf.Clamp(curPos.z, -2.6f, 2.6f);

        transform.position = curPos;

    }
    //void Follow() // 원형을 따라가도록 구현
    //{
    //    agent.destination = playerFollow.position;
    //    // 길찾기 시작
    //    agent.isStopped = false;
    //}

    void Shot()
    {
        if (curShorDelay < maxShotDelay)
            return;

        GameObject bullet = Instantiate(bullet1, transform.position, transform.rotation);
        BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();
        bulletCtrl.player = this;

        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.left * 10, ForceMode.Impulse);
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

            if (clone.TryGetComponent<PlayCtrl>(out var playCtrl))
            {
                Destroy(playCtrl.colliders);
                Destroy(playCtrl.main_Camera);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Add10":
                Destroy(other);
                Add(2);
                break;

            case "Add":
                // Instantiate(prefabToSpawn, transform.position + GetComponent<Collider>().bounds.size.x, Quaternion.identity);
                break;

            case "Sub":
                break;

            case "Mul":
                break;

            case "Div":
                break;
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