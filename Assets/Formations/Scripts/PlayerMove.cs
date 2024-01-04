using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Debug.Log("PlayerMove À§Ä¡ °ª:" + transform.position);
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
}
