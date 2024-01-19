using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public PlayCtrl player;

    private Rigidbody rigid;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.left * 10, ForceMode.Impulse);
        Destroy(gameObject, 3.5f);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyCtrl testEnemy = other.gameObject.GetComponent<EnemyCtrl>();
            testEnemy.OnDamaged(player.ATK);
        }
    }

}
