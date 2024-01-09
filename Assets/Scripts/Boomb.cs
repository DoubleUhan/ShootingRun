using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000f;
    public float fuseTime = 3f; // ��ź�� ����������� ��� �ð�
    ParticleSystem[] part;
    MeshRenderer meshRenderer;

    void Start()
    {
        part = GetComponentsInChildren<ParticleSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(StartFuse());
    }

    IEnumerator StartFuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    void Explode()
    {
        meshRenderer.enabled = false;
        // ���� ȿ�� ���
        foreach (ParticleSystem p in part)
        {
            p.Play();
        }
        
        // �ֺ��� Collider�� ã�Ƽ� ���� ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // ���� ���� ���� �ִ� ������Ʈ���� ���� ȿ�� ����
            if (IsWithinExplosionRange(nearbyObject.transform.position))
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        StartCoroutine(CheckParticleSystems()); // ��ƼŬ �ý����� ���� ������ ���
    }

    bool IsWithinExplosionRange(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        return distance <= explosionRadius;
    }

    IEnumerator CheckParticleSystems()
    {
        // ��� ��ƼŬ �ý����� ��� ���̸� ���
        while (Array.Exists(part, p => p.isPlaying))
        {
            yield return null;
        }

        // ��ƼŬ �ý����� ������ ��ź �ı�
        Destroy(gameObject);
    }
}
