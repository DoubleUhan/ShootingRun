using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Range(1f, 5f)]
    public float speed;
    public PlayCtrl player;
    //ÃÑ¾Ë ÇÁ¸®ÆÕ
    public GameObject bulletPrefab;
    //¹ß»ç °£°Ý
    public float shootInterval;

    private void Start()
    {
        player = FindObjectOfType<PlayCtrl>();
        StartCoroutine(ShootingCor());
    }

    private void Update()
    {
        Move();

    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 0.1f)
            return;
        else
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            dir.y = 0;
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
        }
        //float radius = gameObject.GetComponent<CapsuleCollider>().radius;
        //if(Physics.SphereCast(transform.position, radius, dir, out RaycastHit hit, 1 << LayerMask.NameToLayer("SHOOTER")))
        //{
        //    Debug.Log(hit.point);
        //    dir = (hit.point + (-dir * radius) - transform.position).normalized;
        //    transform.Translate(dir * Time.deltaTime * speed, Space.World);
        //}
        //else
        //{
        //    transform.Translate(dir * Time.deltaTime * speed, Space.World);
        //}
    }

    private IEnumerator ShootingCor()
    {
        WaitForSeconds delay = new WaitForSeconds(shootInterval);
        while(true)
        {
            BulletCtrl bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletCtrl>();
            bullet.player = player;
            yield return delay;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Add10":
                Destroy(other);
                player.Add(2);
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
}
