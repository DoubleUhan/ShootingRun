using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDance : MonoBehaviour
{
    Vector3 target = new Vector3(0f, -15.12f, 0f);
    Quaternion targetRo = Quaternion.Euler(0, 45, 0);



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(5.5f);
        transform.position = Vector3.MoveTowards(transform.position, target, 0.01f); // 보스 위로 이동
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRo, 0.005f);

        yield return new WaitForSeconds(5);
        // 여기서 보스 댄스 끝남
    }
}