using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtrl : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject playerPrefab; // 복제할 플레이어 오브젝트
    float cloneOffset = 2f; // 플레이어 간의 간격

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
    //void OnTriggerEnter(Collider other)
    //{
    //    switch (other.gameObject.tag)
    //    {
    //        case "Add":
    //            ClonePlayers();
    //            GameObject AddPlayer = Instantiate(gameObject, transform.position - new Vector3(0, 0, 1), Quaternion.identity);
    //            break;

    //        case "Sub":
    //            break;

    //        case "Mul":
    //            break;

    //        case "Div":
    //            break;
    //    }
    //}
    //void ClonePlayers()
    //{
    //    // 왼쪽에 플레이어 복제
    //    GameObject leftClonedPlayer = InstantiatePlayer(transform.position - new Vector3(cloneOffset, 0f, 0f));

    //    // 오른쪽에 플레이어 복제
    //    GameObject rightClonedPlayer = InstantiatePlayer(transform.position + new Vector3(cloneOffset, 0f, 0f));
    //}

    //GameObject InstantiatePlayer(Vector3 position)
    //{
    //    // 플레이어 오브젝트를 주어진 위치에 복제
    //    GameObject clonedPlayer = Instantiate(playerPrefab, position, Quaternion.identity);

    //    return clonedPlayer;
    //}
}
