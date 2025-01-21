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

    int count = 5; // ��Ī Ƚ�� - stage 1,2 : 5 , stage 3 : 10
    int health = 5;
    int totalChance = 6; // ���þȷ귿 ��ȸ

    public Animator successAnim; //ī�� ��Ī �� ������̼��߰�

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

    public void Matched() // ī�� ��Ī �ϱ�
    {
        if (firstCard.idx == secondCard.idx) // ��Ī �� ī�尡 ���� ���
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard();

            if (firstCard.idx != 5)
            {
                count--;
            }

            if (count == 0)
            {
                GameClear();
            }

            Debug.Log(count);
        }

        else if (firstCard.idx != secondCard.idx)
        {
            if (firstCard.idx == 5)//��ź�� ���
            {
                MiniGame();
            }

            firstCard.CloseCard();
            secondCard.CloseCard();

        }

        firstCard = null;
        secondCard = null;
    }

    public void MiniGame() // ���þ� �귿
    {
        Shoot();

        if (health == 0)
        {
            GameOver();
        }

        Debug.Log(totalChance);
        Debug.Log(health);

    }

    public void Shoot() // �Ѿ� �ٽ�������? 
    {
        if (Random.Range(0, totalChance) == 0) // �ݹ� ����
        {
            health--;
            totalChance = 6;
            //user ���̱�
        }
        else // �ݹ� ����
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

    public void GameOver()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
