using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public GameObject nextStageBtn;
    public GameObject mainBtn;
    public GameObject endingBtn;

    int count = 5; // 매칭 횟수 - stage 1,2 : 5 , stage 3 : 10
    int health = 5;
    int totalChance = 6; // 러시안룰렛 기회

    public Animator successAnim; //카드 매칭 시 에디메이션추가

    float time;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }
  
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Matched() // 카드 매칭 하기
    {
        if (firstCard.idx == secondCard.idx) // 매칭 한 카드가 같을 경우
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard();

            if (firstCard.idx != 5 || secondCard.idx != 5)
            {
                count--;
            }

            if (count == 0)
            {
                GameClear();// 다음스테이지 또는 
            }

            Debug.Log($"매칭성공수{count}");
        }

        else if (firstCard.idx != secondCard.idx)
        {
            if (firstCard.idx == 5 || secondCard.idx == 5)//폭탄일 경우
            {
                MiniGame();
            }

            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void MiniGame() // 러시안 룰렛
    {
        Shoot();

        if (health == 0)
        {
            //GameOver();
        }

        Debug.Log($"총알남은수{totalChance}");
        Debug.Log($"산사람수{health}");

    }

    public void Shoot() // 총알 다썼을때는? 
    {
        if (Random.Range(0, totalChance) == 0) // 격발 성공
        {
            health--;
            totalChance = 6;
            //user 죽이기
            Debug.Log("한명죽었다");
        }
        else // 격발 실패
        {
            totalChance--;
        }
    }

    public void GameClear()
    { 
        mainBtn.SetActive(true);
        nextStageBtn.SetActive(true);
        endingBtn.SetActive(true);
    }

    //public void GameOver() // 게임종료
    //{
    //    SceneManager.LoadScene("EndingScene");
    //}
}
