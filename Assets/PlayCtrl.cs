using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayCtrl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;
    [SerializeField] float objectDestroy = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Move();
        Shot();
        Reload();
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

    void Shot()
    {
        if (curShorDelay < maxShotDelay)
            return;
        GameObject bullet = Instantiate(bullet1, transform.position, transform.rotation);
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.left * 10, ForceMode.Impulse);
        curShorDelay = 0;

        Destroy(bullet, objectDestroy);
    }

    void Reload()
    {
        curShorDelay += Time.deltaTime;
    }
}
