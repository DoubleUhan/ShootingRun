using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    public GameObject groundPrefab; // ������ ���� ������
    public int numberOfGrounds = 4; // ���ÿ� ǥ�õ� ���� ����

    private List<GameObject> grounds = new List<GameObject>();
    private float groundLength; // ������ ����
    private float spawnZ = 0f; // ���� ������ ������ Z ��ġ
    private float safeZone = 100f; // �÷��̾ ���� �� ���ο� ������ �����ϴ� ��ġ

    void Start()
    {
        // ���� �������� ���̸� ����
        groundLength = groundPrefab.GetComponent<Renderer>().bounds.size.z;

        // �ʱ� ���� ����
        for (int i = 0; i < numberOfGrounds; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        // �÷��̾ ���� ��ġ�� �����ϸ� ���ο� ������ ����
        if (transform.position.z - safeZone > (spawnZ - numberOfGrounds * groundLength))
        {
            SpawnGround();
            DeleteGround();
        }
    }

    void SpawnGround()
    {
        GameObject ground = Instantiate(groundPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += groundLength;
        grounds.Add(ground);
    }

    void DeleteGround()
    {
        Destroy(grounds[0]);
        grounds.RemoveAt(0);
    }
}
