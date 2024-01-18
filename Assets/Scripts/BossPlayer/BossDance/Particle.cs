using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    float wait = 0;
    public ParticleSystem particleObject; //��ƼŬ�ý���

    private void Start()
    {

        particleObject.Stop();

        particleObject = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        Debug.Log("hi");
        wait += Time.deltaTime;
        if (wait >= 7)
        {
            particleObject.Play();
        }
    }
}