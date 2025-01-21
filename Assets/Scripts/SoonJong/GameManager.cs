using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    int totalBullet = 6;

    public Animator successAnim; //ī�� ��Ī �� ������̼��߰�

    float time;
    bool isGameOver = false;

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

    public void Matched() 
    {
        if (firstCard.idx == secondCard.idx) // ��Ī �� ī�尡 ���� ���
        {
             firstCard.DestoryCard();
             secondCard.DestoryCard();
             //successAnim.SetBool("isSuccess", true); // ī�� ��Ī ���� anim
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
        if (Random.Range(0, totalBullet) == 0)
        {
            
        }
    }
}
