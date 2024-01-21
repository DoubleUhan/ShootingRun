using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boomb : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce = 1000f;
    public float fuseTime; // ��ź�� ����������� ��� �ð�

    public BossPlayCtrl player; // ���� �÷��̾�
    public BossCtrl gameend; // BossCtrl ����?

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
        player = GameObject.FindWithTag("Shooter").GetComponent<BossPlayCtrl>();

        //Debug.Log(explosionRadius);

        boss = GameObject.FindGameObjectWithTag("Boss");
        gameend = boss.GetComponent<BossCtrl>();
        meshRenderer = GetComponent<MeshRenderer>();
        //StartCoroutine(StartFuse());
    }

    IEnumerator StartFuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    public void Explode()
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
            }
        }
        gameObject.layer = 0;

        StartCoroutine(Explotion());
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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator Explotion()
    {
        yield return new WaitForSeconds(0.05f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.CompareTag("Shooter"))
            {
                if (IsWithinExplosionRange(nearbyObject.transform.position))
                {
                    if (!gameend.isDead)
                    {
                        Destroy(nearbyObject.gameObject);
                        player.PlayerCount -= 1;
                    }
                    //nearbyObject.gameObject.SetActive(false);
                }
            }
        }
    }
}
