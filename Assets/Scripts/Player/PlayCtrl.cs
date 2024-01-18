using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayCtrl : Stats
{
    public Transform spawnTr;
    public GameObject[] shooterPrefab;
    public List<Shooter> shooterList = new List<Shooter>();
    [SerializeField] float speed;

    [Header("플레이어 공격 관련 변수")]
    // [SerializeField] GameObject bullet1;
    [SerializeField] float maxShotDelay;
    [SerializeField] float curShorDelay;

    [Header("고글 시스템 관련 변수")]
    public Image goggle_Stamina;
    public float decrease_Speed;
    public float fill_Speed;
    public GameObject main_Camera; // 사칙연산, 장애물 안보이는 카메라
    public GameObject arithmetic_Camera; // 사칙연산, 장애물 보이는 카메라
    public GameObject arithmetic_BG; // 고글 착용했을 때 보이게 하는 배경
    public float shiftCooldownDuration = 1.5f;
    private bool shiftCooldown = false;
    public bool isGoggle;

    public Transform[] arithmeticPos; // 0 -> 1번 1-> 2번
    private Vector3 dir;
    int randomNum;

    private void Start()
    {
        randomNum = Random.Range(0, 3);
        PlayerPrefs.SetInt("IsClear", 0);
        PlayerPrefs.SetInt("PlayerCount", 1);
        GameManager.Instance.player_Count = 1;
        GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();

        dir = spawnTr.position;
        Shooter shooter = Instantiate(shooterPrefab[randomNum], spawnTr.position, Quaternion.Euler(0f, -90f, 0f)).GetComponent<Shooter>();
        shooter.transform.SetParent(transform);
        shooter.player = this;
        shooterList.Add(shooter);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Goggle();
    }

    void Move()
    {
        if (PlayerPrefs.GetInt("IsClear") == 0) // IsClear = false
        {
            dir.z = Mathf.Clamp(dir.z + Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, -4.5f, 4.5f);
            transform.position = dir;
        }
        else
        {
            // 현재 위치를 가져오고 x축으로 이동
            Vector3 newPosition = transform.position + new Vector3(speed * -5 * Time.deltaTime, 0f, 0f);
            // 새로운 위치로 이동
            transform.position = newPosition;
        }
    }

    #region 고글 시스템 
    void Goggle()
    {
        // 쿨다운 중이 아니라면 왼쪽 시프트 키 입력 확인
        if (!shiftCooldown && Input.GetKey(KeyCode.LeftShift))
        {
            isGoggle = true;
            arithmetic_BG.SetActive(true);
            arithmetic_Camera.SetActive(true);
            main_Camera.SetActive(false);

            goggle_Stamina.fillAmount -= decrease_Speed * Time.deltaTime;

            // fillAmount이 0이 되었을 때 쿨다운 시작
            if (goggle_Stamina.fillAmount <= 0f)
            {
                StartShiftCooldown();
            }
        }
        else
        {
            isGoggle = false;
            // 쿨다운 중이 아니면 goggle_Stamina의 fillAmount 값을 증가시킴
            arithmetic_BG.SetActive(false);
            arithmetic_Camera.SetActive(false);
            main_Camera.SetActive(true);
            goggle_Stamina.fillAmount += fill_Speed * Time.deltaTime;

            // fillAmount 값을 0과 1 사이로 유지
            goggle_Stamina.fillAmount = Mathf.Clamp(goggle_Stamina.fillAmount, 0f, 1f);
        }
    }

    void StartShiftCooldown()
    {
        shiftCooldown = true;
        StartCoroutine(ShiftCooldownTimer());
    }

    IEnumerator ShiftCooldownTimer()
    {
        yield return new WaitForSeconds(shiftCooldownDuration);

        // 쿨다운이 끝나면 쿨다운 플래그를 비활성화
        shiftCooldown = false;
    }

    #endregion
    #region 사칙연산 
    public void Add(int num)
    {
        for (int i = 0; i < num; i++)
        {
            randomNum = Random.Range(0, 3);
            Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            Shooter clone = Instantiate(shooterPrefab[randomNum], spawnTr.position + randomPos, Quaternion.Euler(0f, -90f, 0f)).GetComponent<Shooter>();
            clone.player = this;
            clone.transform.SetParent(clone.player.transform);
            shooterList.Add(clone);
        }
        GameManager.Instance.player_Count = shooterList.Count;
        GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
        PlayerPrefs.SetInt("PlayerCount", shooterList.Count);
    }
    public void Sub(int num, bool isEnemy)
    {
        if (shooterList.Count <= 1 && !isEnemy)
            return;

        for (int i = 0; i < num; i++)
        {
            if (shooterList.Count > 0)
            {
                // 리스트에서 가장 최근에 생성된 Shooter를 가져오고, 해당 Shooter를 파괴하고 리스트에서 제거
                Shooter shooterToRemove = shooterList[shooterList.Count - 1];
                shooterList.RemoveAt(shooterList.Count - 1);
                Destroy(shooterToRemove.gameObject);
            }

        }
        GameManager.Instance.player_Count = shooterList.Count;
        GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
        PlayerPrefs.SetInt("PlayerCount", shooterList.Count);
        Debug.Log("Sub");
    }

    public void Mult(int num)
    {
        int currentShooterCount = shooterList.Count;
        int totalShooterCount = currentShooterCount * num; // 현재의 복제된 수의 2배

        Debug.Log(currentShooterCount);
        for (int i = 0; i < totalShooterCount - currentShooterCount; i++)
        {
            randomNum = Random.Range(0, 3);
            Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            Shooter clone = Instantiate(shooterPrefab[randomNum], spawnTr.position + randomPos, Quaternion.Euler(0f, -90f, 0f)).GetComponent<Shooter>();
            clone.player = this;
            clone.transform.SetParent(clone.player.transform);
            shooterList.Add(clone);
        }
        GameManager.Instance.player_Count = shooterList.Count;
        GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();

        PlayerPrefs.SetInt("PlayerCount", shooterList.Count);
        Debug.Log("Mult");
    }
    public void Div(int num)
    {
        if (shooterList.Count <= 1)
            return;

        Sub((int)Mathf.Round(shooterList.Count / num), false);

        GameManager.Instance.player_Count = shooterList.Count;
        GameManager.Instance.player_Count_T.text = GameManager.Instance.player_Count.ToString();
        PlayerPrefs.SetInt("PlayerCount", shooterList.Count);
        Debug.Log("Div");
    }

    #endregion
}