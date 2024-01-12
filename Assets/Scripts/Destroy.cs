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
            // 리스트 삭제 코드 입력
            player.shooterList.Remove(oterShooter);
            GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
            Destroy(other.gameObject);
        }
    }
}
