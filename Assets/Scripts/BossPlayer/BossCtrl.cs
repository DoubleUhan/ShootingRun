using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : Stats
{

    float time;
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

    [Range(0,10)]
    public float attackArrange;

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
                    StartCoroutine(SideAttack());
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


        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
        warning.SetActive(false);
        animator.Play("Attack1");
        yield return null;
        // 애니메션 그리고 공격
    }

    IEnumerator SideAttack()
    {
        warning.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        warning.SetActive(true);
        time = 0;
        isSkillActive = true;

        yield return new WaitForSeconds(2f);


        if (warning.GetComponent<BossSkillRange>().isPlayerIn)
        {
            GameFail();
        }
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
        {
            GameClear();
        }

        bossHP_bar.value = curBossHP / maxBossHP;
    }

    void GameFail()
    {
        Camera.main.transform.SetParent(null);
        Destroy(target);
        Time.timeScale = 0;
        failPopup.SetActive(true);
    }

    void GameClear()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
        // 게임 클리어 한 거임 -> 클리어 메세지 또는 화면 출력
        clearPopup.SetActive(true);
    }
}
