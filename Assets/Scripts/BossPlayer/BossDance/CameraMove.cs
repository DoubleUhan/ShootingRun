using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 targetRotation;

    float duration = 2f;

    private float elapsedTime = 0f;

    private void Start()
    {
        targetRotation = new Vector3(20, -90, 0);

        StartCoroutine(InterpolateTransform());
    }

    IEnumerator InterpolateTransform()
    {
        yield return new WaitForSeconds(1f);

        Vector3 initialRotation = transform.eulerAngles;

        while (elapsedTime < duration)
        {
            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;


            // ������ ȸ�� ���
            Vector3 newRotation = Vector3.Lerp(initialRotation, targetRotation, elapsedTime / duration);

            // ���� Transform�� ������ ��ġ�� ȸ�� ����
            transform.eulerAngles = newRotation;

            yield return null;
        }

        transform.eulerAngles = targetRotation;
    }

    void Update()
    {

    }
}

