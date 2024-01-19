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
            // 콜라이더에 진입한 오브젝트를 리스트에 추가
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
            // 콜라이더를 나간 오브젝트를 리스트에서 제거
            if (collidingObjects.Contains(other.gameObject))
            {
                collidingObjects.Remove(other.gameObject);
                // 여기에서 오브젝트를 삭제하거나 다른 작업을 수행할 수 있습니다.
            }
        }
    }
}