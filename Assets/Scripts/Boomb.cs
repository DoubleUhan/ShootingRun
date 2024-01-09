using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000f;
    public float fuseTime = 3f; // 폭탄이 터지기까지의 대기 시간
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
        // 폭발 효과 재생
        foreach (ParticleSystem p in part)
        {
            p.Play();
        }
        
        // 주변의 Collider를 찾아서 폭발 적용
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // 폭발 범위 내에 있는 오브젝트에만 폭발 효과 적용
            if (IsWithinExplosionRange(nearbyObject.transform.position))
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        StartCoroutine(CheckParticleSystems()); // 파티클 시스템이 끝날 때까지 대기
    }

    bool IsWithinExplosionRange(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        return distance <= explosionRadius;
    }

    IEnumerator CheckParticleSystems()
    {
        // 모든 파티클 시스템이 재생 중이면 대기
        while (Array.Exists(part, p => p.isPlaying))
        {
            yield return null;
        }

        // 파티클 시스템이 끝나면 폭탄 파괴
        Destroy(gameObject);
    }
}
