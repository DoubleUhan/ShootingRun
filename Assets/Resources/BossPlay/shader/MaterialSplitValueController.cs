using UnityEngine;

public class MaterialSplitValueController : MonoBehaviour
{
    public Material targetMaterial; // ������ ���׸���
    public float splitValueChangeSpeed = 0.5f; // Split Value ���� �ӵ�

    void Start()
    {
        targetMaterial.SetFloat("_Split_Value", 2f);
        // targetMaterial = GetComponents<Material>();
    }
    void Update()
    {
        // Ű���� �Է��̳� �ٸ� �Է� ������� Split Value�� �����ϰ� �ʹٸ� ������ �����ϼ���.
        float input = Input.GetAxis("Vertical"); // ������ �Է��� ���� (-1���� 1������ ��)

        // Split Value ���� �ǽð����� ����
        float currentSplitValue = targetMaterial.GetFloat("_Split_Value");
        float newSplitValue = Mathf.Clamp(currentSplitValue + input * splitValueChangeSpeed * Time.deltaTime, 0f, 1f);
        targetMaterial.SetFloat("_Split_Value", newSplitValue);
    }
}
