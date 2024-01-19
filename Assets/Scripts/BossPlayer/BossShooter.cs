using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossShooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;

    //�Ѿ� ������
    public GameObject bulletPrefab;
    public BossPlayCtrl player; // ���� �÷��̾�
    public BossCtrl gameend; // BossCtrl ����?

    public GameObject target; // ���� Ÿ��

    public BossPlayCtrl warning1;
    public BossPlayCtrl warning2;

    Rigidbody rb;

    //�߻� ����
    public float shootInterval;

    // ������ �̵� ����
    float tempAngle = 0;
    [SerializeField]
    float angle; // ������ ������ ����
    float radius = 15f; // ���� ������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<BossPlayCtrl>();
        target = GameObject.FindWithTag("target");
        StartCoroutine(ShootingCor());
    }


    void Update()
    {
        // ���纻�� ������
        if (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * Vector3.Distance(transform.position, player.transform.position) * speed;
        }
        transform.LookAt(target.transform.position);

        //Check();
    }

    void Check()
    {
        Debug.Log("����");
        if (warning1.GetComponent<BossRange>().isPlayerIn)
        {
            Debug.Log(warning1);
            Destroy(gameObject);
            player.PlayerCount -= 1;
        }

        else if (warning2.GetComponent<BossRange>().isPlayerIn)
        {
            Debug.Log(warning2);
            Destroy(gameObject);
            player.PlayerCount -= 1;
        }

        else
        {
            return;
        }
    }

    private IEnumerator ShootingCor()
    {
        if (bulletPrefab != null)
        {
            float shotDelay = Random.Range(3f, 6f);
            WaitForSeconds delay = new WaitForSeconds(shotDelay);
            while (true)
            {

                SoundManager.Instance.Gun1();

                BossBulletCtrl bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity).GetComponent<BossBulletCtrl>();
                bullet.GetTarget(target);
                yield return delay;
            }
        }
    }
    public void Hit()
    {
        player.PlayerCount -= 1;
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(other.gameObject.name);
    //    if (other.gameObject.CompareTag("BossPunch"))
    //    {
    //        Debug.Log("���� ����");
    //        Destroy(gameObject);
    //        player.PlayerCount -= 1;
    //        Debug.Log("���� ���̵��� : " + player.PlayerCount);

    //        //// �ֵ� �� ������ ���� ����
    //        //if (player.PlayerCount <= 0)
    //        //{
    //        //    Camera.main.transform.SetParent(null);
    //        //    gameend.GameFail();
    //    }
        //else if (other.gameObject.CompareTag("Bomb"))
        //{
        //    Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);
        //}
    //}
}


//public void Spawn()
//{
//    int random = Random.Range(0, 3);

//    // Ŭ�� ����
//    num = 3; // PlayerPrefs.GetInt("PlayerCount");
//    for (int i = 0; i < num; i++)
//    {
//        Vector3 shooterSpawn = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
//        GameObject spawnShooter = Instantiate(shooter[random].gameObject, shooterSpawn, Quaternion.Euler(0f, -90f, 0f));
//    }
//}

