using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : SingletonMonoBase<SoundManager>
{
    public static SoundManager instance;

    private AudioSource BGM;
    private AudioSource audioSource;

    // ���� ����Ʈ - ������Ʈ �� �־ ������ ��
    [Header("���� �����Ҹ�")]
    AudioClip smile3;
    AudioClip smile1;
    AudioClip smile2;
    AudioClip bomb;

    [Header("���� BGM")]
    AudioClip BossBGM;

    [Header("�� �Ҹ�")]
    AudioClip shot;

    [Header("UI �Ҹ�")]
    AudioClip button;

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
        // ���� ���� ���� ����Ʈ
        smile1 = Resources.Load<AudioClip>("Sounds/laugh 1");
        smile2 = Resources.Load<AudioClip>("Sounds/laugh 2");
        smile3 = Resources.Load<AudioClip>("Sounds/laugh 3");
        bomb = Resources.Load<AudioClip>("Sounds/bomb");

        // BGM ���� ����Ʈ
        BossBGM = Resources.Load<AudioClip>("Sounds/test_BGM");

        // �÷��̾� ���� ���� ����Ʈ
        shot = Resources.Load<AudioClip>("Sounds/shot");

        // UI ���� ���� ����Ʈ
        button = Resources.Load<AudioClip>("Sounds/btn_click");

        // Scene�� �̵� ���� ��, AudioSource Component�� �� �߰��Ǵ� ���� �������� ó��.
        if (audioSource == null && BGM == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            BGM = gameObject.AddComponent<AudioSource>();
        }
        BGM.loop = true;    // ��������� ��� ���;� �ϱ⿡ loop�� ���ش�.
        BGM.volume = 0.2f;      // �Ҹ���ü�� Ŀ�� ����
        audioSource.volume = 0.3f;  // ����� �̱⿡ �� �������� ���� �����Ͽ� ��ü���� �۰� ����
        audioSource.playOnAwake = false;
    }

    public void Boss_Smile() // BossCtrl - 89��
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

    public void Boss_BGM() // BossCtrl - 33��
    {
        if (audioSource == null) return;
        BGM.clip = BossBGM;
        BGM.volume = 0.5f;
        BGM.Play();
    }

    public void Boss_PlayerShot()
    {
        if (audioSource == null) return;
        audioSource.volume = 0.01f;
        audioSource.PlayOneShot(shot);
    }

    public void BTN_Click()
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(button);
    }

    public void Boom()
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(bomb);
    }
}








//private AudioSource BGM;
//private AudioSource audioSource;

//[Header("�����(BGM)")]
//public AudioClip audio1;
//public AudioClip audio2;

//[Header("ȿ����(Effect)")]
//public AudioClip audio3;
//public AudioClip audio4;

//public void Initialize()
//{
//    audio1 = Resources.Load<AudioClip>("Sounds/audio1");
//    audio2 = Resources.Load<AudioClip>("Sounds/audio2");
//    audio3 = Resources.Load<AudioClip>("Sounds/audio3");
//    audio4 = Resources.Load<AudioClip>("Sounds/audio4");

//    // Scene�� �̵� ���� ��, AudioSource Component�� �� �߰��Ǵ� ���� �������� ó��.
//    if (audioSource == null && BGM == null)
//    {
//        audioSource = gameObject.AddComponent<AudioSource>();
//        BGM = gameObject.AddComponent<AudioSource>();
//    }
//    BGM.loop = true;    // ��������� ��� ���;� �ϱ⿡ loop�� ���ش�.
//    BGM.volume = 0.2f;      // �Ҹ���ü�� Ŀ�� ����
//    audioSource.volume = 0.3f;  // ����� �̱⿡ �� �������� ���� �����Ͽ� ��ü���� �۰� ����
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
//    // BGM.Stop(); // ������̶� ��ġ�� �ȵǴ� ������ ����
//    audioSource.PlayOneShot(audio4);
//}