using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtrl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;
    [SerializeField] float objectDestroy = 5.0f;
    public GameObject playerPrefab; // 복제할 플레이어 오브젝트
    float cloneOffset = 2f; // 플레이어 간의 간격

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