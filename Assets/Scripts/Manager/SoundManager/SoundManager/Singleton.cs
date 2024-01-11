using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBase<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // �� ��ü���� Ž��
                _instance = FindObjectOfType<T>();

                // �׷��� ���� ���
                if (_instance == null)
                {
                    GameObject container = new GameObject($"Singleton {typeof(T)}");
                    _instance = container.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    // �������� Instance�� ȣ���Ͽ� �������� �ʰ�,
    // ���� �̸� ��ġ�صδ� ��� ���
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            // ���û��� : Don't Destroy On Load
            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
        else
        {
            // �ν��Ͻ��� �̹� ���������� ������ �ƴ� ���,
            // �����θ� �ı�
            if (_instance != this)
            {
                if (GetComponents<Component>().Length <= 2)
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }

        // �ڽ� Awake ȣ��
        Awake2();
    }

    protected virtual void Awake2() { }
}
