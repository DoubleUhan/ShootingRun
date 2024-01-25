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

        StageManager.Instance.SetNextStagePos();
        transform.position = StageManager.Instance.GetStartPos();
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << LayerMask.NameToLayer("Object"); // "ClickableObject" ���̾ �����ϵ��� ���̾� ����ũ ����

            if (Input.GetMouseButton(0)) /*Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask)*/
            {
                Debug.Log("��Ŭ��");
                MoveToTarget(StageManager.Instance.stageCol.position);

                //if (hit.collider.CompareTag("Road"))
                //{
                //    MoveToTarget(hit.point);
                //    Debug.Log("HIT");
                //}
                //else
                //{
                //    Debug.Log("�ٸ� �� Ŭ��");
                //}
                //if (hit.collider.CompareTag("Stage1") || hit.collider.CompareTag("Stage2") || hit.collider.CompareTag("BossStage"))
                //{
                //}
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
                //   SceneManager.LoadScene("Stage1");
                SceneManager.LoadScene("WantedScene");
                PlayerPrefs.SetInt("Stage", 1);
                break;
            case 1:
                // SceneManager.LoadScene("Stage2");
                SceneManager.LoadScene("WantedScene");
                PlayerPrefs.SetInt("Stage", 2);
                break;
            case 2:
                SceneManager.LoadScene("WantedScene");
                //SceneManager.LoadScene("MOB_BossScene");
                break;
        }
    }
}
