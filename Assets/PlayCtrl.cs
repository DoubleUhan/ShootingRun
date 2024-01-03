using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtrl : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        float h = 0, v = 0;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        var curPos = transform.position;
        curPos += new Vector3(-v, 0, h) * speed * Time.deltaTime;
        curPos.x = Mathf.Clamp(curPos.x, 4f, 7.5f);
        curPos.z = Mathf.Clamp(curPos.z, -2.6f, 2.6f);

        transform.position = curPos;
    }
}
