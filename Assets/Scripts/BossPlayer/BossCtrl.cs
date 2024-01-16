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
    GameObject target; // 플레이어
    [SerializeField] GameObject formatTarget; // 스킬 사용후 바라볼 위치 (가운대)

    [SerializeField] GameObject warning; // 공격 예고 오브젝트

    [Header("팝업 설정")]
    [SerializeField] GameObject failPopup; // 게임 실패 시 뜨는 팝업
    [SerializeField] GameObject clearPopup; // 게임 클리어 시 뜨는 팝업

    [Header("보스 설정")]
    [SerializeField] GameObject bomb; // 떨굴 폭탄
    [SerializeField] GameObject[] bombSpawnPoint;

    [SerializeField] Slider bossHP_bar;
    [SerializeField] float maxBossHP;

    [Header("머리따라가는 에니메이션")]
    [SerializeField] GameObject trackingTarget;

    Animator animator;
    [HideInInspector] public bool isSkillActive; // 보스 스킬 실행 여부
    [HideInInspector] public bool warningOn = false;

    [Header ("카메라 흔들림")]
    public CameraShake cameraShake;

    [Range(0, 10)]
    public float attackArrange;

    float curBossHP; // 보스 체력 설정
    bool isDead; // 보스 죽었는지 안죽었는지 체크하는 변수
    float delay; // 보스 공격 딜레이

    void Start()
    {
        SoundManager.Instance.Boss_BGM();

        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BossAttack());
        animator = GetComponent<Animator>();
        delay = 3f;
        curBossHP = maxBossHP;
    }

    void Update()
    {
        if (isSkillActive)
        {
            Vector3 warningTarget = warning.transform.position;
            warningTarget.y = transform.position.y;
            Vector3 dir = warningTarget - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
        }
        else
        {
            Vector3 warningTarget = warning.transform.position;
            warningTarget.x = transform.position.x;
            Vector3 dir = formatTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
        }
    }



    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(3f);

        while (!isDead)
        {
            int num = UnityEngine.Random.Range(0, 2); // 폭탄 잠깐 끔
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
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator PullDown()
    {
        warning.transform.position = target.transform.position;
        warning.SetActive(true);

        yield return new WaitForSeconds(1f);
        isSkillActive = true;
        
        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.3f); // 공격 시작 0.1초 뒤에 경고 삭제
        warning.SetActive(false);
        animator.SetTrigger("Idle");

        SoundManager.Instance.Boss_Smile(); // 보스 웃음 소리

        yield return null;
    }

    IEnumerator SideAttack()
    {
        warning.transform.position = target.transform.position;
        warning.SetActive(true);

        yield return new WaitForSeconds(1f); // 경고가 뜨고 1초 뒤에 공격 시작
        isSkillActive = true;

        animator.SetTrigger("Attack2");
       
        yield return new WaitForSeconds(0.3f); // 공격 시작 0.1초 뒤에 경고 삭제
        warning.SetActive(false);
        animator.SetTrigger("Idle");

        yield return null;
    }

    public void ShakeCamera() // 카메라 흔들리기
    {
        StartCoroutine(cameraShake.Shake(0.3f, 0.4f));
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
        int randNum = UnityEngine.Random.Range(0, 9);
        GameObject spawnbomb = Instantiate(bomb, bombSpawnPoint[randNum].transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }


    void SkillEnded()
    {
        isSkillActive = false;
    }

    void PlayHurt() // 애니메이션 이벤트에 넣어서 플레이어 공격 맞나 체크하는 함수
    {

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
        //target.GetComponent<Renderer>().enabled = false;
        target.SetActive(false);
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