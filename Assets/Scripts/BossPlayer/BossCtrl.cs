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
using UnityEngine.Windows;

public class BossCtrl : Stats
{
    GameObject target; // 플레이어

    [SerializeField]
    GameObject formatTarget; // 스킬 사용후 바라볼 위치 (가운대)

    [SerializeField]
    GameObject warning1; // 패턴 1 공격 예고 오브젝트

    [SerializeField]
    GameObject warning2; // 패턴 2 공격 예고 오브젝트

    [SerializeField]
    GameObject toBoss; // 패턴 2 공격 예고 각도(방향) 맞추는 용도

    [Header("팝업 설정")]
    [SerializeField]
    GameObject failPopup; // 게임 실패 시 뜨는 팝업

    [SerializeField]
    GameObject clearPopup; // 게임 클리어 시 뜨는 팝업

    [Header("보스 설정")]
    [SerializeField]
    GameObject bomb; // 떨굴 폭탄

    [SerializeField]
    GameObject[] bombSpawnPoint;

    [SerializeField]
    Image bossHP_bar;

    [SerializeField]
    float maxBossHP;

    [Header("머리따라가는 에니메이션")]
    [SerializeField]
    GameObject trackingTarget;

    Animator animator;

    [HideInInspector]
    public bool isSkillActive; // 보스 스킬 실행 여부

    [HideInInspector]
    public bool warningOn = false;

    [Header("카메라 흔들림")]
    public CameraShake cameraShake;

    [Header("폭탄 터짐")]
    public List<Boomb> bombs = new List<Boomb>();

    bool bombInstall;

    [Range(0, 10)]
    public float attackArrange;

    float curBossHP; // 보스 체력 설정

    [HideInInspector]
    public bool isDead; // 보스 죽었는지 안죽었는지 체크하는 변수
    float delay; // 보스 공격 딜레이

    public List<Material> targetMaterials; // 조절할 머테리얼

    AudioSource BGaudio;

    void Start()
    {
        Time.timeScale = 1;

        SoundManager.Instance.Boss_BGM();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                targetMaterials.Add(m);
            }
        }

        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BossAttack());
        StartCoroutine(Laugh());
        animator = GetComponent<Animator>();
        delay = 3f;
        curBossHP = maxBossHP;

        bombInstall = false;
    }

    void Update()
    {
        BossDeahAnimation();

        if (isSkillActive)
        {
            Vector3 warningTarget = warning1.transform.position;
            warningTarget.y = transform.position.y;
            Vector3 dir = warningTarget - transform.position;

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * 5
            );
        }
        else
        {
            Vector3 warningTarget = warning1.transform.position;
            warningTarget.x = transform.position.x;
            Vector3 dir = formatTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * 3
            );
        }
    }

    IEnumerator Laugh()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.Instance.Boss_Smile(); // 보스 웃음 소리
            yield return new WaitForSeconds(15f);
        }
        yield return null;
    }

    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(3f);

        while (!isDead)
        {
            int num = UnityEngine.Random.Range(0, 4);
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

                case 3:
                    if (bombInstall == true)
                    {
                        StartCoroutine(Boom());
                    }
                    break;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator PullDown()
    {
        warning1.transform.position = target.transform.position;
        warning1.SetActive(true);

        yield return new WaitForSeconds(1f);
        isSkillActive = true;

        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.8f); // 공격 시작 0.1초 뒤에 경고 삭제

        List<GameObject> collidingObjects = warning1.GetComponent<BossRange>().collidingObjects;

        if (collidingObjects.Count > 0)
        {
            foreach (var collidingObject in collidingObjects)
            {
                if (collidingObject != null)
                {
                    if (!isDead)
                    {
                        collidingObject.GetComponent<BossShooter>().Hit();
                    }
                }
            }
        }

        warning1.SetActive(false);
        animator.SetTrigger("Idle");

        yield return null;
    }

    IEnumerator SideAttack()
    {
        warning2.transform.position = target.transform.position;
        warning2.transform.LookAt(toBoss.transform.position); // 각도 맞추기
        //warning2.transform.rotation = target.transform.rotation;
        warning2.SetActive(true);

        yield return new WaitForSeconds(0.7f); // 경고가 뜨고 1초 뒤에 공격 시작
        isSkillActive = true;

        animator.SetTrigger("Attack2");

        yield return new WaitForSeconds(1.0f); // 공격 시작 0.1초 뒤에 경고 삭제

        List<GameObject> collidingObjects = warning2.GetComponent<BossRange>().collidingObjects;

        if (collidingObjects.Count > 0)
        {
            foreach (var collidingObject in collidingObjects)
            {
                if (collidingObject != null)
                {
                    if (!isDead)
                    {
                        collidingObject.GetComponent<BossShooter>().Hit();
                    }
                }
            }
        }

        warning2.SetActive(false);
        animator.SetTrigger("Idle");

        yield return null;
    }

    public void ShakeCamera() // 카메라 흔들리기
    {
        StartCoroutine(cameraShake.Shake(0.3f, 0.4f));
    }

    IEnumerator Bomb() // 애니메, 사운드 없음ㄴ
    {
        int random = UnityEngine.Random.Range(3, 6);

        for (int i = 0; i < random; i++)
        {
            BombSpawn();
        }
        yield return null;
    }

    void BombSpawn()
    {
        int randNum = UnityEngine.Random.Range(0, 9);
        Boomb spawnbomb = Instantiate(
                bomb,
                bombSpawnPoint[randNum].transform.position,
                Quaternion.Euler(-90f, 0f, 0f)
            )
            .GetComponent<Boomb>();
        bombs.Add(spawnbomb);
        bombInstall = true;
    }

    IEnumerator Boom() // 폭탄 터치는 함수 패턴 4
    {
        animator.SetTrigger("Attack3");

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Idle");

        yield return null;
    }

    public void BoomBoom()
    {
        foreach (Boomb bomb in bombs)
        {
            bomb.Explode();
        }
        bombs.Clear();
        bombInstall = false;
    }

    void SkillEnded()
    {
        isSkillActive = false;
    }

    //void PlayHurt() // 애니메이션 이벤트에 넣어서 플레이어 공격 맞나 체크하는 함수
    //{

    //}
    public void OnDamaged(float Damage)
    {
        if (isDead)
            return;

        curBossHP -= Damage;

        if (curBossHP <= 0)
        {
            isDead = false;
            // 보스 죽는 애니메이션
            SoundManager.Instance.Stop(); // 사운드 전부 중지
            SoundManager.Instance.BossDie();
            StartCoroutine(WaitForClearAnimation());
        }

        bossHP_bar.fillAmount = curBossHP / maxBossHP;
    }

    public void BossDeahAnimation()
    {
        if (isDead)
        {
            foreach (Material targetMaterial in targetMaterials)
            {
                float currentSplitValue = targetMaterial.GetFloat("_Split_Value");
                float newSplitValue = Mathf.Lerp(currentSplitValue, 0, Time.deltaTime * .5f);
                targetMaterial.SetFloat("_Split_Value", newSplitValue);
            }
        }
    }

    public void GameFail()
    {
        //target.GetComponent<Renderer>().enabled = false;
        target.SetActive(false);
        SoundManager.Instance.BGMStop();
        failPopup.SetActive(true);
        SoundManager.Instance.Fail();
        Time.timeScale = 0;
    }

    void GameClear()
    {
        SoundManager.Instance.BGMStop();
        // 게임 클리어 한 거임 -> 클리어 메세지 또는 화면 출력
        clearPopup.SetActive(true);
        SoundManager.Instance.Clear();
        Time.timeScale = 0;
    }

    IEnumerator WaitForClearAnimation()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2f);

        isDead = true;

        yield return new WaitForSeconds(4f);

        GameClear();
    }
}
