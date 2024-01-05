using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [Range(1f, 5f)]
    public float speed;
    [HideInInspector]
    public PlayCtrl player;
    //�Ѿ� ������
    public GameObject bulletPrefab;
    //�߻� ����
    public float shootInterval;

    private Rigidbody rb;

    private void Start()
    {
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
            rb.velocity = dir * speed;
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
                player.Add(10);
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
