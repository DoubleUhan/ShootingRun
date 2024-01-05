using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillRange : MonoBehaviour
{
    public bool isPlayerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) isPlayerIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerIn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
