using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [Range(1f, 30f)]
    public float speed;
    [HideInInspector]
    public PlayCtrl player;
    //ÃÑ¾Ë ÇÁ¸®ÆÕ
    public GameObject bulletPrefab;
    //¹ß»ç °£°Ý
    public float shootInterval;

    private Rigidbody rb;

    private void Start()
    {
        tag= "Player";
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
            rb.velocity = dir *  Vector3.Distance(transform.position, player.transform.position) * speed;
        }
    }

    private IEnumerator ShootingCor()
    {
        WaitForSeconds delay = new WaitForSeconds(shootInterval);
        while (true)
        {
            BulletCtrl bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletCtrl>();
            bullet.player = player;
            yield return delay;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        int value = 0;
        switch (other.gameObject.tag)
        {
            case "Add3":
            case "Add5":
            case "Add10":
                value = int.Parse(tag.Substring(3));
                player.Add(value);
                Destroy(other.gameObject);
                break;

            case "Sub3":
            case "Sub5":
            case "Sub10":
                value = int.Parse(tag.Substring(3));
                player.Sub(value);
                Destroy(other.gameObject);
                break;

            case "Mult":
                player.Mult();
                Destroy(other.gameObject);
                break;

            case "Div":
                player.Div();
                Destroy(other.gameObject);
                break;
        }
    }
}
