using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    [Header("���� ���� ������")]
    public float delay;

    public GameObject target; // �ٶ� Ÿ���̶� ��ġ���̵�
    public GameObject formatTarget; // �ٽ� ��� �����ϴ� Ÿ��

    public GameObject warning; // ���� ���� ����

    public GameObject failPopup; // ���� ���� �� �ߴ� �˾�
    public GameObject clearPopup; // ���� Ŭ���� �� �ߴ� �˾�

    public GameObject bomb; // ���� ��ź

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // ���� ü�� ����

    public GameObject trackingTarget;

    Animator animator;
    public static bool isSkillActive;
    public static bool warningOn = false;
    bool isDead; // ���� �׾����� ���׾����� üũ�ϴ� ����

    [Range(0, 10)]
    public float attackArrange;

    MultiAimConstraint multiAimConstraint;
    [SerializeField] GameObject[] bombSpawnPoint;

    void Start()
    {
        multiAimConstraint = trackingTarget.GetComponent<MultiAimConstraint>();
        SoundManager.Instance.Boss_BGM();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BossAttack());
        delay = 3.0f;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(Time.timeScale);
        if (isSkillActive)
        {
            
            if (multiAimConstraint.data.sourceObjects.GetWeight(0).Equals(1))
            {
                var a = multiAimConstraint.data.sourceObjects;
                a.SetWeight(0, 0);
                a.SetWeight(1, 1);
                multiAimConstraint.data.sourceObjects = a;
            }

            Vector3 warningTarget = warning.transform.position;
            warningTarget.y = transform.position.y;
            Vector3 dir = warningTarget - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
        }
        else
        {
            if (multiAimConstraint.data.sourceObjects.GetWeight(0).Equals(0))
            {
                var a = multiAimConstraint.data.sourceObjects;
                a.SetWeight(0, 1);
                a.SetWeight(1, 0);
                multiAimConstraint.data.sourceObjects = a;
            }
            Vector3 dir = formatTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
        }

        //if (isSkillActive == false)
        //{
        //    Vector3 dir = target.transform.position - transform.position;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);

        //    // ���� ���� ��
        //    //transform.LookAt(target.transform);
        //    // var a = transform.rotation;
        //    //a = new Quaternion(0, transform.rotation.y, 0,0);
        //    //Debug.Log("a "+ transform.rotation);
        //    //transform.localRotation = new Quaternion(0, transform.rotation.y, 0, 0);
        //    //Debug.Log(transform.rotation);
        //}
        //else
        //{
        //}
    }



    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(3f);
        while (!isDead)
        {
            int num = UnityEngine.Random.Range(0, 3);
            switch (num)
            {
                case 0: // ���� ���
                    StartCoroutine(PullDown());
                    break;

                case 1: // �۾���
                    StartCoroutine(SideAttack());
                    break;

                case 2: // ��ź ������
                    StartCoroutine(Bomb());
                    break;
            }
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator PullDown()
    {
        warning.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        warning.SetActive(true);
        isSkillActive = true;

        yield return new WaitForSeconds(1f);
        warning.SetActive(false);

        animator.Play("ATK_pattern_1");

        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
        SoundManager.Instance.Boss_Smile(); // ���� ���� �Ҹ�

        yield return null;
    }

    IEnumerator SideAttack()
    {
        warning.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        warning.SetActive(true);
        isSkillActive = true;

        yield return new WaitForSeconds(1f);


        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
        warning.SetActive(false);

        animator.Play("ATK_pattern_2");        

        yield return null;
    }
    IEnumerator Bomb() // �ִϸ�, ���� ������
    {
        int randNum = UnityEngine.Random.Range(0, 9);
        Debug.Log("randNum: " + randNum);
        GameObject spawnbomb = Instantiate(bomb, bombSpawnPoint[randNum].transform.position, Quaternion.identity);
        yield return null;
    }

    void SkillEnded()
    {
        isSkillActive = false;
    }
    public void OnDamaged(float Damage)
    {
        if (isDead)
            return;

        curBossHP -= Damage;

        if (curBossHP <= 0)
        {
            isDead = true;
            // ���� �״� �ִϸ��̼�
            animator.Play("Death");
            GameClear();
        }

        bossHP_bar.value = curBossHP / maxBossHP;
    }

    void GameFail()
    {
        Camera.main.transform.SetParent(null);
        target.GetComponent<Renderer>().enabled = false;
        failPopup.SetActive(true);
        Time.timeScale = 0;
    }

    void GameClear()
    {
        // ���� Ŭ���� �� ���� -> Ŭ���� �޼��� �Ǵ� ȭ�� ���
        clearPopup.SetActive(true);
        Time.timeScale = 0;
    }
}