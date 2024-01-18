using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;
    public PlayCtrl player;
    //총알 프리팹
    public GameObject bulletPrefab;

    public GameObject playerEffect;
    //발사 간격
    public float shootInterval;

    private Rigidbody rb;

    private void Start()
    {
        tag = "Player";
        rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(ShootingCor());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * Vector3.Distance(transform.position, player.transform.position) * speed;
        }
    }

    private IEnumerator ShootingCor()
    {
        if (bulletPrefab != null)
        {
            WaitForSeconds delay = new WaitForSeconds(shootInterval);
            while (true)
            {
                if (!player.isGoggle) // Check if isGoggle is false
                {
                    GameObject playerEffectObj = Instantiate(playerEffect, player.transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                    Destroy(playerEffectObj, 0.5f);
                    BulletCtrl bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity).GetComponent<BulletCtrl>();
                    bullet.player = player;
                }

                yield return delay;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gameEnd)
            return;

        string tag = other.gameObject.tag;

        if (other.CompareTag("ArithmeticTag"))
        {
            var artic = other.GetComponent<Arithmetic>();
            var pair = artic.Pair;

            if (!artic.IsEatable) return; //  연산자 못먹는 상태 

            if(Vector3.Distance(pair.transform.position, transform.position) <
                Vector3.Distance(artic.transform.position, transform.position))
            {
                return;
            }

            if(pair != null) pair.IsEatable = false;

            switch (artic.type)
            {
                case ArithmeticType.add:
                    player.Add(artic.value);
                    break;
                case ArithmeticType.sub:
                    player.Sub(artic.value,false);
                    break;
                case ArithmeticType.mult:
                    player.Mult(artic.value);
                    break;
                case ArithmeticType.div:
                    player.Div(artic.value);
                    break;
            }
            Destroy(other.gameObject);
        }
        

        if (other.CompareTag("Enemy"))
        {
            player.Sub(1,true);
            if (player.shooterList.Count <= 0)
            {
                GameManager.Instance.OverStage();
            }
            Destroy(other.gameObject);
        }
    }
}
