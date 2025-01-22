using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard; // 처음 뒤집은 카드
    public Card secondCard; // 두번째 뒤집은 카드 

    public float score;

    //public GameObject nextStageBtn;
    //public GameObject mainBtn;
    //public GameObject endingBtn;

    int count = 5; // 매칭 남은 횟수  - stage 1,2 : 5 , stage 3 : 10
    int health = 5; // 살아있는 사람 수
    int totalChance = 6; // 러시안룰렛 기회

    //public Animator successAnim; //카드 매칭 시 에디메이션추가

    float time;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        if (firstCard.index == secondCard.index) // 매칭 한 카드가 같을 경우
        {
            firstCard.DestoryCard();  
            secondCard.DestoryCard(); // 사람카드든 폭탄카드든 맞으면 파괴

            if (firstCard.index != 10 || secondCard.index != 10)  // 매칭 한 카드가 사람카드일 경우 매칭 횟수 차감
            {
                count--;

                Debug.Log($"클리어까지 남은 수{count}");
            }

            if (count == 0) //매칭 완료 클리어
            {
                Debug.Log("클리어!!");
                //GameClear();
            }

            
        }

        else if (firstCard.index != secondCard.index) // 매칭 한 카드가 다를 경우
        {
            if (firstCard.index ==  10 || secondCard.index == 10) // 폭탄이 하나가 있으면 미니게임 진행
            {
                MiniGame();
            }

            firstCard.CloseCard();  
            secondCard.CloseCard();  //뒤집은 카드 정보 초기화
        }

        firstCard = null;
        secondCard = null;
    }

    public void MiniGame() // 러시안 룰렛
    {
        Shoot(); //총알 격발

        if (health == 0)
        {
            //GameOver(); // 어떻게진행?
            Debug.Log($"모두 죽었습니다");
        }

        Debug.Log($"총알남은수{totalChance}");
        Debug.Log($"산사람수{health}");

    }

    public void Shoot()
    {
        if (Random.Range(0, totalChance) == 0) // 격발 성공
        {
            health--;
            totalChance = 6;
            //user 죽이기
            Debug.Log("한명죽었다");
        }
        else 
        {
            totalChance--; // 격발 실패 시 총알감소
        }
    }

    /*public void GameClear()
    { 
        mainBtn.SetActive(true);
        nextStageBtn.SetActive(true);
        endingBtn.SetActive(true);
    }*/

    //public void GameOver() // 게임종료
    //{
    //    SceneManager.LoadScene("EndingScene");
    //}
}
