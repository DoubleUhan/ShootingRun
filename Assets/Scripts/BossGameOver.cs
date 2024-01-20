using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BossGameOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetObject; // 활성화/비활성화할 대상 오브젝트

    // 마우스를 올렸을 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // 오브젝트를 활성화합니다.
        }
    }

    // 마우스를 떼었을 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // 오브젝트를 비활성화합니다.
        }
    }
}
