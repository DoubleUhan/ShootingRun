using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    public GameObject[] BossAttackRange;

    float time;
    public float delay;

    public GameObject target; // �ٶ� Ÿ���̶� ��ġ���̵�

    public GameObject warning; // ���� ���� ����

    public GameObject lookTarget; // �ٶ� Ÿ��

    bool isSkillActive;

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // ���� ü�� ����

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
                case 0: // ���� ���
                    StartCoroutine(PullDown());
                    break;

                case 1: // �۾���
                    break;

                case 2: // ��ź ������
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
        // �ִϸ޼� �׸��� ����
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
