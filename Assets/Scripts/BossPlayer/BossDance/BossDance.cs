using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDance : MonoBehaviour
{
    Vector3 target = new Vector3(0f, -15.12f, 0f);
    Quaternion targetRo = Quaternion.Euler(0, 45, 0);

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(5.5f);
        transform.position = Vector3.MoveTowards(transform.position, target, 0.05f); // ���� ���� �̵�
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRo, 0.005f);

        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene("MOB_BossScene");
        // ���⼭ ���� ���� ����
    }
}
