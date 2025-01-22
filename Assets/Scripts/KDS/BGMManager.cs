using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;  // 싱글톤 인스턴스
    public AudioSource audioSource;      // BGM을 재생할 AudioSource
    public AudioClip bgmClip;           // 재생할 BGM 트랙
    
    
    // 싱글톤 인스턴스를 반환
    public static BGMManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BGMManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        // 싱글톤 패턴을 사용하여 BGMManager가 하나만 존재하도록 보장
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 BGMManager가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 다른 인스턴스를 파괴
        }
    }

    void Start()
    {
        // BGM 시작
        PlayBGM();
    }

    // BGM을 시작하는 함수
    public void PlayBGM()
    {
        // 이미 음악이 재생되고 있다면, 새로 시작하지 않음
        if (!audioSource.isPlaying)
        {
            if (audioSource != null && bgmClip != null)
            {
                // 첫 시작 시 BGM 트랙을 설정하고 재생
                audioSource.clip = bgmClip;
                audioSource.loop = true;  // 반복 재생 설정
                audioSource.Play();
            }
        }
    }
}