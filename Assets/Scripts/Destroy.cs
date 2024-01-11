using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public PlayCtrl player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayCtrl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Shooter oterShooter = other.GetComponent<Shooter>();
            // ����Ʈ ���� �ڵ� �Է�
            player.shooterList.Remove(oterShooter);
            Destroy(other.gameObject);
        }
    }
}
