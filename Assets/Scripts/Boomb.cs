using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 1000f;
    public float fuseTime = 3f; // 폭탄이 터지기까지의 대기 시간
    Transform[] pieces;
    MeshRenderer meshRenderer;
    GameObject[] players;

    void Start()
    {
        
        pieces = GetComponentsInChildren<Transform>();
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(pieces);
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
                Rigidbody rb = piece.GetComponent<Rigidbody>();

                piece.GetComponent<MeshRenderer>().enabled = true;
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        foreach (var p in players)
        {
            if(Vector3.Distance(p.transform.position, transform.position) < explosionRadius)
            {
                if (p != null)
                {
                    Destroy(p.gameObject);
                }
            }
        }

        StartCoroutine(DeleteObject());
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
