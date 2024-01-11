using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boomb : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce = 1000f;
    public float fuseTime = 3f; // 폭탄이 터지기까지의 대기 시간

    // public GameObject effect;

    Transform[] pieces;
    MeshRenderer meshRenderer;
    GameObject boss;

    void Awake()
    {

        pieces = GetComponentsInChildren<Transform>();
        foreach (Transform t in pieces)
        {
            if (t != transform)
            {
                t.gameObject.SetActive(false);
            }

        }
    }

    void Start()
    {
        Debug.Log(explosionRadius);

        boss = GameObject.FindGameObjectWithTag("Boss");
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

        foreach (var piece in pieces)
        {
            if (piece != transform)
            {
                piece.gameObject.SetActive(true);
                Rigidbody rb = piece.GetComponent<Rigidbody>();

                piece.GetComponent<MeshRenderer>().enabled = true;
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                // effect.SetActive(true);
            }
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.CompareTag("Player"))
            {
                if (IsWithinExplosionRange(nearbyObject.transform.position))
                {
                    boss.GetComponent<BossCtrl>().GameFail();
                }
            }
            // 폭발 범위 내에 있는 오브젝트에만 폭발 효과 적용
        }



        StartCoroutine(DeleteObject());
    }

    bool IsWithinExplosionRange(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        return distance <= explosionRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
