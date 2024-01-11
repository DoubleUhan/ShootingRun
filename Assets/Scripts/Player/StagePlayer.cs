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
            int layerMask = 1 << LayerMask.NameToLayer("Object"); // "ClickableObject" 레이어만 감지하도록 레이어 마스크 설정

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("우클릭");

                if (hit.collider.CompareTag("Stage1") || hit.collider.CompareTag("Stage2"))
                {
                    MoveToTarget(hit.point);
                }
                else
                {
                    Debug.Log("다른 곳 클릭");
                    // 다른 곳을 클릭했을 때 추가적인 동작을 수행하거나 무시할 수 있습니다.
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
                SceneManager.LoadScene("Stage1");
                break;
            case "Stage2":
                StageManager.instance.ArriveStage(2);
                break;
        }
    }
}
