using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

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
                
            }

            firstCard.CloseCard();
            secondCard.CloseCard();

        }

        firstCard = null;
        secondCard = null;
    }
}
