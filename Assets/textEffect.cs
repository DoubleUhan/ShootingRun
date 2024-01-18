using UnityEngine;
using UnityEngine.UI;

public class textEffect : MonoBehaviour
{
    public Text tapToStartText;
    public float fadeDuration = 2.0f; // ���̵� �� �� ���̵� �ƿ� ���� �ð� (��)

    private float timer = 0f;
    private bool increasing = true;

    void Update()
    {
        // Ÿ�̸� ���� �Ǵ� ����
        if (increasing)
            timer += Time.deltaTime;
        else
            timer -= Time.deltaTime;

        // ���İ��� õõ�� ��ȭ���Ѽ� ��Ÿ����
        float alpha = Mathf.Clamp01(timer / fadeDuration);
        tapToStartText.color = new Color(tapToStartText.color.r, tapToStartText.color.g, tapToStartText.color.b, alpha);

        // ���İ��� 0 �Ǵ� 1�� �����ϸ� ������ ����
        if (alpha <= 0f || alpha >= 0.7f)
            increasing = !increasing;
    }
}
