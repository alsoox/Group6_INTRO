using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;
    public int cardCount=0;
    float time = 0.0f;
    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= 30f)
        {
            GameOver();
        }
    }
    public void Matched()
    {
        if(firstCard.idx==secondCard.idx)
        {
  
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            audioSource.PlayOneShot(clip);
            if (cardCount ==0)
            {
                GameOver();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        endTxt.SetActive(true);
    }
}
