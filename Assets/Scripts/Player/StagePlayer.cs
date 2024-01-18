using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class StagePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hit;

    bool isArrive;
    void Start()
    {
        Time.timeScale = 1;
        agent = GetComponent<NavMeshAgent>();

        transform.position = StageManager.Instance.GetStartPos();
        StageManager.Instance.SetNextStagePos();
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

                if (hit.collider.CompareTag("Stage1") || hit.collider.CompareTag("Stage2") || hit.collider.CompareTag("BossStage"))
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
        switch (StageManager.Instance.clearStageMax)
        {
            case 0:
                SceneManager.LoadScene("Stage1");
                PlayerPrefs.SetInt("Stage", 1);
                break;
            case 1:
                SceneManager.LoadScene("Stage2");
                PlayerPrefs.SetInt("Stage", 2);
                break;
            case 2:
                SceneManager.LoadScene("MOB_BossScene");
                break;
        }
    }
}
