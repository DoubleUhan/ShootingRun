using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : Stats
{
    public GameObject[] BossAttackRange;
    void Start()
    {
        StartCoroutine(BossAttack());
    }
    void Update()
    {

    }

    IEnumerator BossAttack()
    {
        int num = 0; /*Random.Range(0, 3);*/
        while (true)
        {
            switch (num)
            {
                case 0: // ���� ���
                    break;

                case 1: // �۾���

                    break;
                case 2: // ��ź ������

                    break;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
