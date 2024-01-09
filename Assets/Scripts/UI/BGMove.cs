using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        // 현재 위치를 가져오고 x축으로 이동
        Vector3 newPosition = transform.position + direction * Time.deltaTime * speed;
        // 새로운 위치로 이동
        transform.position = newPosition;
    }
}
