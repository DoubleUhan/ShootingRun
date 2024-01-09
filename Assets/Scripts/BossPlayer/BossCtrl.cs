using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    [Header("보스 공격 딜레이")]
    public float delay;

    public GameObject target; // 바라볼 타겟이랑 겹치느ㅜ듯

    public GameObject warning; // 빨간 예고 범위

    public GameObject failPopup; // 게임 실패 시 뜨는 팝업
    public GameObject clearPopup; // 게임 클리어 시 뜨는 팝업

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // 보스 체력 설정

    Animator animator;
    bool isSkillActive;
    bool isDead; // 보스 죽었는지 안죽었는지 체크하는 변수

    [Range(0, 10)]
    public float attackArrange;

    void Start()
    {
        SoundManager.Instance.Boss_BGM();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BossAttack());
        delay = 3.0f;
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Debug.Log(Time.timeScale);
        if (isSkillActive == false)
        {
            //transform.LookAt(target.transform);
            // var a = transform.rotation;
            //a = new Quaternion(0, transform.rotation.y, 0,0);
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
            //Debug.Log("a "+ transform.rotation);
            //transform.localRotation = new Quaternion(0, transform.rotation.y, 0, 0);
            //Debug.Log(transform.rotation);
        }
        else
        {
            Vector3 dir = warning.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
        }
    }



    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(3f);
        int num = 0; // Random.Range(0, 3);
        while (!isDead)
        {
            // Random.Range(0, 3);
            switch (num)
            {
                case 0: // 내려 찍기
                    StartCoroutine(PullDown());
                    break;

                case 1: // 휩쓸기
                    StartCoroutine(SideAttack());
                    break;

                case 2: // 폭탄 던지기
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


        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
        warning.SetActive(false);
        animator.Play("ATK_pattern_1");
        SoundManager.Instance.Boss_Smile(); // 사운드매니저에서 불러옴
        Debug.Log("공격 소리 났다");
        yield return null;
        // 애니메션 그리고 공격
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
        // 애니메션 그리고 공격
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