using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    public GameObject groundPrefab; // 생성할 지형 프리팹
    public int numberOfGrounds = 4; // 동시에 표시될 지형 개수

    private List<GameObject> grounds = new List<GameObject>();
    private float groundLength; // 지형의 길이
    private float spawnZ = 0f; // 다음 지형을 생성할 Z 위치
    private float safeZone = 100f; // 플레이어가 멈출 때 새로운 지형을 생성하는 위치

    void Start()
    {
        // 지형 프리팹의 길이를 측정
        groundLength = groundPrefab.GetComponent<Renderer>().bounds.size.z;

        // 초기 지형 생성
        for (int i = 0; i < numberOfGrounds; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        // 플레이어가 일정 위치에 도달하면 새로운 지형을 생성
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
