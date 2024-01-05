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
        Destroy(gameObject, 5f);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other);
            TestEnemy testEnemy = other.gameObject.GetComponent<TestEnemy>();
            testEnemy.OnDamaged(player.ATK);
        }
    }

}
