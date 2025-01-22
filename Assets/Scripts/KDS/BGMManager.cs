using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;  // �̱��� �ν��Ͻ�
    public AudioSource audioSource;      // BGM�� ����� AudioSource
    public AudioClip bgmClip;           // ����� BGM Ʈ��
    
    
    // �̱��� �ν��Ͻ��� ��ȯ
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
        // �̱��� ������ ����Ͽ� BGMManager�� �ϳ��� �����ϵ��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ BGMManager�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ٸ� �ν��Ͻ��� �ı�
        }
    }

    void Start()
    {
        // BGM ����
        PlayBGM();
    }

    // BGM�� �����ϴ� �Լ�
    public void PlayBGM()
    {
        // �̹� ������ ����ǰ� �ִٸ�, ���� �������� ����
        if (!audioSource.isPlaying)
        {
            if (audioSource != null && bgmClip != null)
            {
                // ù ���� �� BGM Ʈ���� �����ϰ� ���
                audioSource.clip = bgmClip;
                audioSource.loop = true;  // �ݺ� ��� ����
                audioSource.Play();
            }
        }
    }
}