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
                case 0: // ³»·Á Âï±â
                    break;

                case 1: // ÈÛ¾µ±â

                    break;
                case 2: // ÆøÅº ´øÁö±â

                    break;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
