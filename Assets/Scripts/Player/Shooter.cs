using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;
    public PlayCtrl player;
    //ÃÑ¾Ë ÇÁ¸®ÆÕ
    public GameObject bulletPrefab;

    public GameObject playerEffect;
    //¹ß»ç °£°Ý
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
                GameObject playerEffectObj = Instantiate(playerEffect, player.transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity); // ÃÑ¾Ë ¹ß»ç Á÷Àü ÀÌÆåÆ® Àû¿ë
                Destroy(playerEffectObj, 0.5f);
                BulletCtrl bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletCtrl>();
                bullet.player = player;
                yield return delay;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;

        if (other.CompareTag("ArithmeticTag"))
        {
            var artic = other.GetComponent<Arithmetic>();
            switch (artic.type)
            {
                case ArithmeticType.add:
                    player.Add(artic.value);
                    break;
                case ArithmeticType.sub:
                    player.Sub(artic.value);
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
    }
}
