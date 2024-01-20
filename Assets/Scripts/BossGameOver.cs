using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BossGameOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetObject; // Ȱ��ȭ/��Ȱ��ȭ�� ��� ������Ʈ

    // ���콺�� �÷��� �� ȣ��Ǵ� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // ������Ʈ�� Ȱ��ȭ�մϴ�.
        }
    }

    // ���콺�� ������ �� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        }
    }
}
