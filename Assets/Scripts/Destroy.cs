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
        // ȣ������ ������ �÷��̾� ������� �ڵ� -> �������׵� ����
        if (other.gameObject.CompareTag("Player"))
        {
            Shooter oterShooter = other.GetComponent<Shooter>();
            // ����Ʈ ���� �ڵ� �Է�
            player.shooterList.Remove(oterShooter);
            GameManager.Instance.player_Count = player.shooterList.Count;
            GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
            Destroy(other.gameObject);

            if (GameManager.Instance.player_Count <= 0)
            {
                Debug.Log("���� ����");
                GameManager.Instance.OverStage();
            }
        }
    }
}
