using UnityEngine;
using UnityEngine.UI;

public class textEffect : MonoBehaviour
{
    public Text tapToStartText;
    public float fadeDuration = 2.0f; // 페이드 인 및 페이드 아웃 지속 시간 (초)

    private float timer = 0f;
    private bool increasing = true;

    void Update()
    {
        // 타이머 증가 또는 감소
        if (increasing)
            timer += Time.deltaTime;
        else
            timer -= Time.deltaTime;

        // 알파값을 천천히 변화시켜서 나타내기
        float alpha = Mathf.Clamp01(timer / fadeDuration);
        tapToStartText.color = new Color(tapToStartText.color.r, tapToStartText.color.g, tapToStartText.color.b, alpha);

        // 알파값이 0 또는 1에 도달하면 방향을 변경
        if (alpha <= 0f || alpha >= 0.7f)
            increasing = !increasing;
    }
}
