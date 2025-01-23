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

    public Card firstCard; // ó�� ������ ī��
    private bool isFirstCard;
    public Card secondCard; // �ι�° ������ ī�� 
    private bool isSecondCard;
    public Board board;
    public bool isRouletteAction;
    public RussianRoulette RussianRouletteAction;
    public RoundAnim roundAnim; // RoundAnim ����
    public GameObject die_text;
    private GameObject currentdie_text;
    public int round = 1;
    public string user_name="default";
    public bool[] isLive = new bool[5] { true, true, true, true, true};
    //jhn : 0 / kds : 1 / ksj : 2 / /shc : 3 / pjw : 4

    int count = 5; // 
    int health = 5; // ����ִ� ��� ��
    int totalChance = 6; // ���þȷ귿 ��ȸ
    int matchingCount = 0; //��Ī������
    int score = 0; // ��ü����

    //public Animator successAnim; //ī�� ��Ī �� ������̼��߰�

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


    public void Matched() // ī�� ��Ī �ϱ� �׷��� �����̸� �߰���
    {
        StartCoroutine(MatchedWithDelay());
    }

    private IEnumerator MatchedWithDelay()
    {
        if (firstCard.index == secondCard.index) // ��Ī �� ī�尡 ���� ���
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard(); // ���ī��� ��źī��� ������ �ı�

            if (firstCard.index != 10 || secondCard.index != 10)  // ��Ī �� ī�尡 ���ī���� ��� ��Ī Ƚ�� ����
            {
                count--;
                matchingCount++;

                Debug.Log($"��Ī ���� : {matchingCount} / ���� ��Ī : {count}");
            }

            if (count == 0) //��Ī �Ϸ� Ŭ����
            {
                Debug.Log($"{round}stage Ŭ����!! {health}�����!!");
                RoundClear();
            }
        }

        else if (firstCard.index != secondCard.index) // ��Ī �� ī�尡 �ٸ� ���
        {
            if (firstCard.index == 10 || secondCard.index == 10) // ��ź�� �ϳ��� ������ �̴ϰ��� ����
            {
                MiniGame();
            }

            firstCard.CloseCard();
            secondCard.CloseCard();  //������ ī�� ���� �ʱ�ȭ

        }

        firstCard = null;
        secondCard = null;
        yield return new WaitForSeconds(0.8f);
    }



    public void MiniGame() // ���þ� �귿
    {
        
        //���� ������ ī�� �ε��� ���� �ش��ϴ� isLive�� true �϶�
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
            rectTransform.localPosition = Vector3.zero; // ĵ���� ������ (0, 0, 0) ��ġ
            Invoke("DestroyCanvas", 1f);
        }


        if (health == 0)
        {
            Invoke("GameOver", 6f);
            GameEnd();
        }

        Debug.Log($"�Ѿ˳�����{totalChance}");
        Debug.Log($"������{health}");

    }

    public void Shoot(int index)
    {
        if (Random.Range(0, totalChance) == 0) // �ݹ� ����
        {
            health--;
            totalChance = 6;//�ݹ߽� �Ѿ� Ƚ�� �ʱ�ȭ

            //user ���̱�(��ź ������ ī�忡 �ش��ϴ� user ���̱�)
            isLive[index] = false;
            RussianRouletteAction.ShootAction(true, index);


        }
        else 
        {
            totalChance--; // �ݹ� ���� �� �Ѿ˰���
            RussianRouletteAction.ShootAction(false, index);
        }
    }

    public void RoundClear()
    {
        round++;  // round �� ����
        board.RoundClear(round); // ������ round ���� �Ѱ���

        if (round == 3)
        {
            count = 10;
        }
        else if (round == 4) //���� Ŭ�����(count, �������� �ʱ�ȭ)
        {
            Debug.Log("4����� �Խ��ϴ�");

            roundAnim.TurnCameraAnimation();
            Debug.Log("TurnCameraAnimation�� �����");
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
        board.RoundClear(round); // ������ round ���� �Ѱ���
    }

    public void GameEnd() // ���ӿ��� (���� ������ �ʱ�ȭ)
    {
        Score();
        Debug.Log($"GameOver : score : {score} ���� :{health} ��Ī�� : {matchingCount}");

        // �ִϸ��̼� �Ϸ� �� ũ���� �� �ε�
        SceneManager.LoadScene("CreditScene");

        // ���� �ʱ�ȭ
        isLive = Enumerable.Repeat(true, isLive.Length).ToArray();
        count = 5;
        health = 5;
        score = 0;
        matchingCount = 0;
    }

    private IEnumerator WaitForCameraAnimationAndLoadScene()
    {
        Debug.Log("��ٷ�");
        while (roundAnim.doAnim)
        {
            yield return null;  // doAnim�� false�� �� ������ ���
            Debug.Log("ī�޶� �ִϸ��̼���");
        }
        // �ִϸ��̼� �Ϸ� �� ũ���� �� �ε�
        Debug.Log("ũ���� �� ���ߵ�");
        SceneManager.LoadScene("CreditScene");
    }



    private void DestroyCanvas()
    {
        Destroy(currentdie_text); // ĵ���� �ı�
    }
    public void Score()
    {
        score = health * 100 + matchingCount * 25; // ������� * 100 + ��Ī�� * 25;
    }

}
