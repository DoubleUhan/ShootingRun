using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : Stats
{
    [Header("���� ���� ������")]
    public float delay;

    public GameObject target; // �ٶ� Ÿ���̶� ��ġ���̵�

    public GameObject warning; // ���� ���� ����

    public GameObject failPopup; // ���� ���� �� �ߴ� �˾�
    public GameObject clearPopup; // ���� Ŭ���� �� �ߴ� �˾�

    public Slider bossHP_bar;

    [SerializeField] float maxBossHP;
    [SerializeField] float curBossHP; // ���� ü�� ����

    Animator animator;
    bool isSkillActive;
    bool isDead; // ���� �׾����� ���׾����� üũ�ϴ� ����

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
                case 0: // ���� ���
                    StartCoroutine(PullDown());
                    break;

                case 1: // �۾���
                    StartCoroutine(SideAttack());
                    break;

                case 2: // ��ź ������
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
        SoundManager.Instance.Boss_Smile(); // ����Ŵ������� �ҷ���
        Debug.Log("���� �Ҹ� ����");
        yield return null;
        // �ִϸ޼� �׸��� ����
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
        // �ִϸ޼� �׸��� ����
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
            // ���� �״� �ִϸ��̼�
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
        // ���� Ŭ���� �� ���� -> Ŭ���� �޼��� �Ǵ� ȭ�� ���
        clearPopup.SetActive(true);
        Time.timeScale = 0;
    }
}