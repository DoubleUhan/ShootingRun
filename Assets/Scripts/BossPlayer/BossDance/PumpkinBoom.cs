using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoom : MonoBehaviour
{

    public float explosionRadius;
    public float explosionForce = 1000f;

    Transform[] pieces;
    MeshRenderer meshRenderer;

    private float trans;


    private void Awake()
    {
        pieces = GetComponentsInChildren<Transform>();
        meshRenderer = GetComponent<MeshRenderer>();

        foreach (Transform t in pieces)
        {
            if (t != transform)
            {
                t.gameObject.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        trans = transform.position.y;

        if (trans <= -8)
        {
            Explode();
        }
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
            }
        }
        StartCoroutine(DeleteObject());
    }

    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
