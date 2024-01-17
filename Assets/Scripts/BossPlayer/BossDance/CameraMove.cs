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
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;


            // 보간된 회전 계산
            Vector3 newRotation = Vector3.Lerp(initialRotation, targetRotation, elapsedTime / duration);

            // 현재 Transform에 보간된 위치와 회전 적용
            transform.eulerAngles = newRotation;

            yield return null;
        }

        transform.eulerAngles = targetRotation;
    }

    void Update()
    {

    }
}

