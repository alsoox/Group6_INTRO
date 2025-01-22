using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private Dictionary<string, AudioClip> soundDict;  // SFX�� BGM�� ������ Dictionary
    public AudioSource sfxPlayer;                   // SFX ����� AudioSource
    public AudioSource bgmPlayer;                   // BGM ����� AudioSource

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips; // ����� Ŭ�� �迭

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayBGM("Blue_Soul-royalty_free_soul_blues");
        // bgmPlayer.volume = 0.5f; // ��ݼҸ� �ʹ� Ŀ�� ������ ����
    }

    private void Init()
    {
        soundDict = new Dictionary<string, AudioClip>();
        bgmPlayer.loop = true; // BGM�� �⺻������ �ݺ� ���

        // Dictionary �ʱ�ȭ
        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }
    }

    // SFX ���
    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            sfxPlayer.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }

    // BGM ���
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.Play();
            }
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }
}