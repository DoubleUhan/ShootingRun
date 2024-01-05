using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : Stats
{
    public GameObject[] BossAttackRange;

    float time;
    public float delay;

    public GameObject target;

    public GameObject warning;

    public GameObject lookTarget;
    bool isSkillActive;

    Animator animator;

    void Start()
    {
        StartCoroutine(BossAttack());
        time = 0.0f;
        delay = 3.0f;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (isSkillActive == false)
        {
            // transform.LookAt(target.transform.position);
            Vector3 dir = target.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
        }
    }



    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(3f);
        int num = 0; /*Random.Range(0, 3);*/
        while (true)
        {
            // Random.Range(0, 3);
            switch (num)
            {
                case 0: // 내려 찍기
                    StartCoroutine(PullDown());
                    break;

                case 1: // 휩쓸기
                    break;

                case 2: // 폭탄 던지기
                    break;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator PullDown()
    {
        warning.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        warning.SetActive(true);
        time = 0;
        isSkillActive = true;
        yield return new WaitForSeconds(2f);
        warning.SetActive(false);
        animator.Play("Attack1");
        yield return null;
        // 애니메션 그리고 공격
    }
    void asasd()
    {
        isSkillActive = false;
    }
}
