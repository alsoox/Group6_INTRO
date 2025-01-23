//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard; // 처음 뒤집은 카드
    public Card secondCard; // 두번째 뒤집은 카드 
    public Board board;
    public GameObject die_text;
    private GameObject currentdie_text;
    public bool isRouletteAction;
    public RussianRoulette RussianRouletteAction;
    public int round = 1;
    public bool[] isLive = new bool[5] { true, true, true, true, true};

     //jhn : 0 / kds : 1 / ksj : 2 / /shc : 3 / pjw : 4
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

                Debug.Log($"매칭 성공 / 남은 매칭 : {count}");
            }

            if (count == 0) //매칭 완료 클리어
            {
                Debug.Log($"{round}stage 클리어!! {health}명생존!!");
                RoundClear();
                if (round == 4)
                {
                    SceneManager.LoadScene("CreditScene");
                }
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
        
        //슛은 뒤집은 카드 인덱스 값에 해당하는 isLive가 true 일때
        if(firstCard.index != 10 && isLive[firstCard.index] == true)
        {
            Shoot(firstCard.index);
        }
        else if (secondCard.index != 10 && isLive[secondCard.index] == true)
        {
            Shoot(secondCard.index);
        }
        else 
        {
            currentdie_text = Instantiate(die_text);
            RectTransform rectTransform = currentdie_text.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero; // 캔버스 내에서 (0, 0, 0) 위치
            Invoke("DestroyCanvas", 1f);
        }

        if (health == 0)
        {
            Invoke("GameOver", 6f);
            Debug.Log($"모두 죽었습니다");
        }

        Debug.Log($"총알남은수{totalChance}");
        Debug.Log($"산사람수{health}");

    }

    public void Shoot(int index)
    {
        if (Random.Range(0, totalChance) == 0) // 격발 성공
        {
            health--;
            totalChance = 6;//격발시 총알 횟수 초기화

            //user 죽이기(폭탄 제외한 카드에 해당하는 user 죽이기)
            isLive[index] = false;
            RussianRouletteAction.ShootAction(true, index);


        }
        else 
        {
            totalChance--; // 격발 실패 시 총알감소
            RussianRouletteAction.ShootAction(false, index);
        }
    }

    public void RoundClear()
    {
        round++;  // round 값 증가
        board.RoundClear(round); // 증가된 round 값을 넘겨줌

        if (round == 3)
        {
            count = 10;
        }
        else if (round == 4) //게임 클리어시(count, 생존여부 초기화)
        {
            isLive = Enumerable.Repeat(true, isLive.Length).ToArray();
            count = 5;
            SceneManager.LoadScene("CreditScene");
        }

        else if (true)
        {
            count = 5;
        }
    }

    public void RoundRetry()
    {
        board.RoundClear(round); // 증가된 round 값을 넘겨줌
    }

    public void GameOver() // 클리어실패 (isLive, count, health 초기화)
    {
        isLive = Enumerable.Repeat(true, isLive.Length).ToArray();
        Debug.Log("실패ㅜㅠ");
        SceneManager.LoadScene("CreditScene");
        count = 5;
        health = 5;
    }
    private void DestroyCanvas()
    {
        Destroy(currentdie_text); // 캔버스 파괴
    }

}
