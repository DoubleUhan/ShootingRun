using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    [Header("보스 공격 딜레이")]
    public float delay;

    public GameObject target; // 바라볼 타겟이랑 겹치느ㅜ듯
    public GameObject formatTarget; // 다시 가운데 보게하는 타겟

    public GameObject warning; // 빨간 예고 범위

    public GameObject failPopup; // 게임 실패 시 뜨는 팝업
    public GameObject clearPopup; // 게임 클리어 시 뜨는 팝업

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // 보스 체력 설정

    public GameObject trackingTarget;

    Animator animator;
    public static bool isSkillActive;
    public static bool warningOn = false;
    bool isDead; // 보스 죽었는지 안죽었는지 체크하는 변수

    [Range(0, 10)]
    public float attackArrange;

    MultiAimConstraint multiAimConstraint;
    RigBuilder rigBuilder;

    void Start()
    {
        multiAimConstraint = trackingTarget.GetComponent<MultiAimConstraint>();
        SoundManager.Instance.Boss_BGM();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BossAttack());
        delay = 3.0f;
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
    }

    void Update()
    {
        Debug.Log(Time.timeScale);
        if (warningOn == true)
        {
            Vector3 warningTarget = warning.transform.position;
            warningTarget.y = transform.position.y - 5;
            Vector3 dir = warningTarget - transform.position;
            if (multiAimConstraint.data.sourceObjects.GetWeight(0).Equals(1))
            {
                var a = multiAimConstraint.data.sourceObjects;
                a.SetWeight(0, 0);
                a.SetWeight(1, 1);
                multiAimConstraint.data.sourceObjects = a;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
        }
        else if (warningOn == false)
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

        //    // 뭔지 이제 모름
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
            int num = 0; // Random.Range(0, 3);
            switch (num)
            {
                case 0: // 내려 찍기
                    yield return StartCoroutine(PullDown());
                    break;

                case 1: // 휩쓸기
                    StartCoroutine(SideAttack());
                    break;

                case 2: // 폭탄 던지기
                    StartCoroutine(Bomb());
                    break;
            }
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator PullDown()
    {
        warningOn = true;
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
        SoundManager.Instance.Boss_Smile(); // 보스 웃음 소리

        Debug.Log("공격 소리 났다");
        yield return new WaitForSeconds(1.33f); // 애니메이션 실행 시간
        warningOn = false;

        yield return null;
    }

    IEnumerator SideAttack()
    {
        warning.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        warning.SetActive(true);
        isSkillActive = true;

        yield return new WaitForSeconds(2f);


        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
        warning.SetActive(false);
        animator.Play("Attack2");
        yield return null;
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(2f);
    }

    void asasd()
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
            // 보스 죽는 애니메이션
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
        // 게임 클리어 한 거임 -> 클리어 메세지 또는 화면 출력
        clearPopup.SetActive(true);
        Time.timeScale = 0;
    }
}