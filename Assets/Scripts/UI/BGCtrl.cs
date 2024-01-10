using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    Queue<Transform> transforms = new Queue<Transform>();

    public int numberOfGrounds = 4; // 동시에 표시될 지형 개수

    public Transform[] grounds;
    public Transform endPos;
    public Transform startPos;

    void Start()
    {
        foreach (Transform ground in grounds)
        {
            transforms.Enqueue(ground);
        }
    }

    void Update()
    {
        if (transforms.Count <= 0)
            return;

        //Debug.Log(Vector3.Distance(transforms.Peek().position, endPos.position));

        if (Vector3.Distance(transforms.Peek().position, endPos.position) <= 0.5f)
        {
            MoveBack(transforms.Dequeue());
        }

    }

    void MoveBack(Transform target)
    {
        // 이동
        target.position = startPos.position;

        transforms.Enqueue(target);
    }

    void Move()
    {

    }

}
