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
    [Header("보스 공격 딜레이")]
    public float delay;

    public GameObject target; // 바라볼 타겟이랑 겹치느ㅜ듯
    public GameObject formatTarget; // 다시 가운데 보게하는 타겟

    public GameObject warning; // 빨간 예고 범위

    public GameObject failPopup; // 게임 실패 시 뜨는 팝업
    public GameObject clearPopup; // 게임 클리어 시 뜨는 팝업

    public GameObject bomb; // 떨굴 폭탄

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

            Vector3 warningTarget = warning.transform.position;
            warningTarget.x = transform.position.x;
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
            int num = UnityEngine.Random.Range(0, 3);
            switch (num)
            {
                case 0: // 내려 찍기
                    StartCoroutine(PullDown());
                    break;

                case 1: // 휩쓸기
                    StartCoroutine(SideAttack());
                    break;

                case 2: // 폭탄 던지기
                    StartCoroutine(Bomb());
                    break;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator PullDown()
    {
        warning.transform.position = target.transform.position;
        warning.SetActive(true);

        yield return new WaitForSeconds(1f);
        isSkillActive = true;
        
        animator.Play("ATK_pattern_1");
        yield return new WaitForSeconds(0.3f); // 공격 시작 0.1초 뒤에 경고 삭제
        warning.SetActive(false);

        SoundManager.Instance.Boss_Smile(); // 보스 웃음 소리

        yield return null;
    }

    IEnumerator SideAttack()
    {
        warning.transform.position = target.transform.position;
        warning.SetActive(true);

        yield return new WaitForSeconds(1f); // 경고가 뜨고 1초 뒤에 공격 시작
        isSkillActive = true;

        animator.Play("ATK_pattern_2");
        yield return new WaitForSeconds(0.3f); // 공격 시작 0.1초 뒤에 경고 삭제
        warning.SetActive(false);

        yield return null;
    }
    IEnumerator Bomb() // 애니메, 사운드 없음ㄴ
    {
        int bombs = UnityEngine.Random.Range(3, 6);

        for (int bomb = 0; bomb < bombs; bomb++)
        {
            BombSpawn();
        }
        yield return null;
    }

    void BombSpawn()
    {
        //int randNum = UnityEngine.Random.Range(0, 9);
        //GameObject spawnbomb = Instantiate(bomb, bombSpawnPoint[randNum].transform.position, Quaternion.identity);

        int randNum = UnityEngine.Random.Range(0, 9);
        GameObject spawnbomb = Instantiate(bomb, bombSpawnPoint[randNum].transform.position, Quaternion.Euler(-90f, 0f, 0f));

    }


    void SkillEnded()
    {
        isSkillActive = false;
    }

    void PlayHurt() // 애니메이션 이벤트에 넣어서 플레이어 공격 맞나 체크하는 함수
    {
        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
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

    public void GameFail()
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