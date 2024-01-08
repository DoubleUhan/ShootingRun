using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : SingletonMonoBase<SoundManager>
{
    public static SoundManager instance;

    private AudioSource BGM;
    private AudioSource audioSource;

    // ���� ����Ʈ
    public AudioClip audio_1;

    private SoundManager()
    {
        instance = this;
    }
    public void Initalize()
    {
        // ���� ����Ʈ
        audio_1 = Resources.Load<AudioClip>("Sounds/audio_1");
        // Scene�� �̵� ���� ��, AudioSource Component�� �� �߰��Ǵ� ���� �������� ó��.
        if (audioSource == null && BGM == null)
        {
            Debug.Log("����");
            audioSource = gameObject.AddComponent<AudioSource>();
            BGM = gameObject.AddComponent<AudioSource>();
        }
        BGM.loop = true;    // ��������� ��� ���;� �ϱ⿡ loop�� ���ش�.
        BGM.volume = 0.2f;      // �Ҹ���ü�� Ŀ�� ����
        audioSource.volume = 0.3f;  // ����� �̱⿡ �� �������� ���� �����Ͽ� ��ü���� �۰� ����
        audioSource.playOnAwake = false;
    }

    public void Attack()
    {
        Debug.Log(audio_1);
        audioSource.PlayOneShot(audio_1);
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