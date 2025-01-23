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
    private bool isFirstCard;
    public Card secondCard; // 두번째 뒤집은 카드 
    private bool isSecondCard;
    public Board board;
    public bool isRouletteAction;
    public RussianRoulette RussianRouletteAction;
    public RoundAnim roundAnim; // RoundAnim 참조
    public GameObject die_text;
    private GameObject currentdie_text;
    public int round = 1;
    public string user_name="default";
    public bool[] isLive = new bool[5] { true, true, true, true, true};
    //jhn : 0 / kds : 1 / ksj : 2 / /shc : 3 / pjw : 4

    int count = 5; // 
    int health = 5; // 살아있는 사람 수
    int totalChance = 6; // 러시안룰렛 기회
    int matchingCount = 0; //매칭성공수
    int score = 0; // 전체점수

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


    public void Matched() // 카드 매칭 하기 그런데 딜레이를 추가한
    {
        StartCoroutine(MatchedWithDelay());
    }

    private IEnumerator MatchedWithDelay()
    {
        if (firstCard.index == secondCard.index) // 매칭 한 카드가 같을 경우
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard(); // 사람카드든 폭탄카드든 맞으면 파괴

            if (firstCard.index != 10 || secondCard.index != 10)  // 매칭 한 카드가 사람카드일 경우 매칭 횟수 차감
            {
                count--;
                matchingCount++;

                Debug.Log($"매칭 성공 : {matchingCount} / 남은 매칭 : {count}");
            }

            if (count == 0) //매칭 완료 클리어
            {
                Debug.Log($"{round}stage 클리어!! {health}명생존!!");
                RoundClear();
            }
        }

        else if (firstCard.index != secondCard.index) // 매칭 한 카드가 다를 경우
        {
            if (firstCard.index == 10 || secondCard.index == 10) // 폭탄이 하나가 있으면 미니게임 진행
            {
                MiniGame();
            }

            firstCard.CloseCard();
            secondCard.CloseCard();  //뒤집은 카드 정보 초기화

        }

        firstCard = null;
        secondCard = null;
        yield return new WaitForSeconds(0.8f);
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
            GameEnd();
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
            Debug.Log("4라운드로 왔습니다");

            roundAnim.TurnCameraAnimation();
            Debug.Log("TurnCameraAnimation이 실행됨");
            Invoke("GameEnd", 5f);
            //WaitForCameraAnimationAndLoadScene();
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

    public void GameEnd() // 게임오버 (게임 정보값 초기화)
    {
        Score();
        Debug.Log($"GameOver : score : {score} 산사람 :{health} 매칭수 : {matchingCount}");

        // 애니메이션 완료 후 크레딧 씬 로드
        SceneManager.LoadScene("CreditScene");

        // 게임 초기화
        isLive = Enumerable.Repeat(true, isLive.Length).ToArray();
        count = 5;
        health = 5;
        score = 0;
        matchingCount = 0;
    }

    private IEnumerator WaitForCameraAnimationAndLoadScene()
    {
        Debug.Log("기다려");
        while (roundAnim.doAnim)
        {
            yield return null;  // doAnim이 false가 될 때까지 대기
            Debug.Log("카메라 애니메이션중");
        }
        // 애니메이션 완료 후 크레딧 씬 로드
        Debug.Log("크레딧 씬 떠야됨");
        SceneManager.LoadScene("CreditScene");
    }



    private void DestroyCanvas()
    {
        Destroy(currentdie_text); // 캔버스 파괴
    }
    public void Score()
    {
        score = health * 100 + matchingCount * 25; // 남은사람 * 100 + 매칭수 * 25;
    }

}
