using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : SingletonMonoBase<SoundManager>
{
    public static SoundManager instance;

    private AudioSource BGM;
    private AudioSource audioSource;

    // 사운드 리스트
    [Header("보스 웃음소리")]
    public AudioClip smile1;
    public AudioClip smile2;
    public AudioClip smile3;

    [Header("보스 BGM")]
    public AudioClip BossBGM;

    [Header("총 소리")]
    public AudioClip shot;

    private void Start()
    {
        Initalize();
    }

    private SoundManager()
    {
        instance = this;
    }
    public void Initalize()
    {
        // 사운드 리스트
        smile1 = Resources.Load<AudioClip>("Sounds/laugh 1");
        smile2 = Resources.Load<AudioClip>("Sounds/laugh 2");
        smile3 = Resources.Load<AudioClip>("Sounds/laugh 3");

        BossBGM = Resources.Load<AudioClip>("Sounds/test_BGM");

        shot = Resources.Load<AudioClip>("Sounds/shot");
        // Scene을 이동 했을 때, AudioSource Component가 또 추가되는 것을 막기위한 처리.
        if (audioSource == null && BGM == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            BGM = gameObject.AddComponent<AudioSource>();
        }
        BGM.loop = true;    // 배경음악은 계속 나와야 하기에 loop를 켜준다.
        BGM.volume = 0.2f;      // 소리자체가 커서 조절
        audioSource.volume = 0.3f;  // 모바일 이기에 폰 볼륨으로 조절 가능하여 자체에서 작게 설정
        audioSource.playOnAwake = false;
    }

    public void Boss_Smile() // BossCtrl - 89줄
    {
        int random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                audioSource.PlayOneShot(smile1);
                break;
            case 2:
                audioSource.PlayOneShot(smile2);
                break;
            case 3:
                audioSource.PlayOneShot(smile3);
                break;
        }
    }

    public void Boss_BGM() // BossCtrl - 33줄
    {
        BGM.clip = BossBGM;
        BGM.volume = 0.5f;
        BGM.Play();
    }

    public void Boss_PlayerShot()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(shot);
    }
}








//private AudioSource BGM;
//private AudioSource audioSource;

//[Header("배경음(BGM)")]
//public AudioClip audio1;
//public AudioClip audio2;

//[Header("효과음(Effect)")]
//public AudioClip audio3;
//public AudioClip audio4;

//public void Initialize()
//{
//    audio1 = Resources.Load<AudioClip>("Sounds/audio1");
//    audio2 = Resources.Load<AudioClip>("Sounds/audio2");
//    audio3 = Resources.Load<AudioClip>("Sounds/audio3");
//    audio4 = Resources.Load<AudioClip>("Sounds/audio4");

//    // Scene을 이동 했을 때, AudioSource Component가 또 추가되는 것을 막기위한 처리.
//    if (audioSource == null && BGM == null)
//    {
//        audioSource = gameObject.AddComponent<AudioSource>();
//        BGM = gameObject.AddComponent<AudioSource>();
//    }
//    BGM.loop = true;    // 배경음악은 계속 나와야 하기에 loop를 켜준다.
//    BGM.volume = 0.2f;      // 소리자체가 커서 조절
//    audioSource.volume = 0.3f;  // 모바일 이기에 폰 볼륨으로 조절 가능하여 자체에서 작게 설정
//    audioSource.playOnAwake = false;
//}

//public void audio1()
//{
//    audioSource.PlayOneShot(audio1);
//}
//public void audio2()
//{
//    audioSource.PlayOneShot(audio2);
//}
//public void audio3()
//{
//    audioSource.PlayOneShot(audio4);
//}
//public void audio4()
//{
//    // BGM.Stop(); // 배경음이랑 겹치면 안되는 사운드라면 쓰기
//    audioSource.PlayOneShot(audio4);
//}