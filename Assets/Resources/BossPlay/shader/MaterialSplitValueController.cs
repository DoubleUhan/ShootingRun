using UnityEngine;

public class MaterialSplitValueController : MonoBehaviour
{
    public Material targetMaterial; // 조절할 머테리얼
    public float splitValueChangeSpeed = 0.5f; // Split Value 변경 속도

    void Start()
    {
        targetMaterial.SetFloat("_Split_Value", 2f);
        // targetMaterial = GetComponents<Material>();
    }
    void Update()
    {
        // 키보드 입력이나 다른 입력 방법으로 Split Value를 조절하고 싶다면 조건을 변경하세요.
        float input = Input.GetAxis("Vertical"); // 수직축 입력을 받음 (-1에서 1까지의 값)

        // Split Value 값을 실시간으로 조절
        float currentSplitValue = targetMaterial.GetFloat("_Split_Value");
        float newSplitValue = Mathf.Clamp(currentSplitValue + input * splitValueChangeSpeed * Time.deltaTime, 0f, 1f);
        targetMaterial.SetFloat("_Split_Value", newSplitValue);
    }
}
