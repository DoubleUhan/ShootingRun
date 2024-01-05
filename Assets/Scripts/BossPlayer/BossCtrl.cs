using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    public GameObject[] BossAttackRange;

    float time;
    public float delay;

    public GameObject target; // 바라볼 타겟이랑 겹치느ㅜ듯

    public GameObject warning; // 빨간 예고 범위

    public GameObject lookTarget; // 바라볼 타겟

    bool isSkillActive;

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // 보스 체력 설정

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

            bossHP_bar.value = curBossHP / maxBossHP;

            Debug.Log(curBossHP);
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
    public void OnDamaged(float Damage)
    {
        curBossHP -= Damage;
        if (curBossHP <= 0)
            Destroy(gameObject);
    }
}
