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
        // 호박한테 맞으면 플레이어 사라지는 코드 -> 보스한테도 적용
        if (other.gameObject.CompareTag("Player"))
        {
            Shooter oterShooter = other.GetComponent<Shooter>();
            // 리스트 삭제 코드 입력
            player.shooterList.Remove(oterShooter);
            GameManager.Instance.player_Count = player.shooterList.Count;
            GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
            Destroy(other.gameObject);

            if (GameManager.Instance.player_Count <= 0)
            {
                Debug.Log("게임 종료");
                GameManager.Instance.OverStage();
            }
        }
    }
}
