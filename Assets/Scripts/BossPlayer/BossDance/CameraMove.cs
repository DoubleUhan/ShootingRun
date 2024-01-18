using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float shake;

    private void Start()
    {
        StartCoroutine(CamShake(0.5f, 1f));
    }
    public IEnumerator CamShake(float duration, float magnitude) // duration: ��鸲 ���� �ð�  magnitude: ��鸲�� ����
    {
        yield return new WaitForSeconds(shake);
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration) //  elapsed: ���� ������ ���� ����� �ð�
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}

