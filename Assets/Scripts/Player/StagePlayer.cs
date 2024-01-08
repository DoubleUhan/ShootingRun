using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StagePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hit;

    bool isArrive;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int layerMask = 1 << LayerMask.NameToLayer("Object"); // "ClickableObject" ���̾ �����ϵ��� ���̾� ����ũ ����

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("��Ŭ��");

                if (hit.collider.CompareTag("Stage"))
                {
                    MoveToTarget(hit.point);
                }
                else
                {
                    Debug.Log("�ٸ� �� Ŭ��");
                    // �ٸ� ���� Ŭ������ �� �߰����� ������ �����ϰų� ������ �� �ֽ��ϴ�.
                }
            }
        }
    }

    void MoveToTarget(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Stage1":
                StageManager.instance.ArriveStage(1);
                break;
            case "Stage2":
                
                break;
            case "Stage3":

                break;
        }
    }
}
