using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject lookTarget;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookTarget.transform.position);

        Vector3 vector = lookTarget.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vector).normalized;
    }
}
