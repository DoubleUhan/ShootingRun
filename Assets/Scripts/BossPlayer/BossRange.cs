using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRange : MonoBehaviour
{
    public bool isPlayerIn = false;

    public List<GameObject> collidingObjects = new List<GameObject>();



   private void OnTriggerEnter(Collider other)
   {
        Debug.Log(collidingObjects);
        if (other.gameObject.CompareTag("Shooter"))
        {
            // �ݶ��̴��� ������ ������Ʈ�� ����Ʈ�� �߰�
            if (!collidingObjects.Contains(other.gameObject))
            {
                collidingObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(collidingObjects);

        if (other.gameObject.CompareTag("Shooter"))
        {
            // �ݶ��̴��� ���� ������Ʈ�� ����Ʈ���� ����
            if (collidingObjects.Contains(other.gameObject))
            {
                collidingObjects.Remove(other.gameObject);
                // ���⿡�� ������Ʈ�� �����ϰų� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
            }
        }
    }
}