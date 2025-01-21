using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

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

    public void Matched() 
    {
        if (firstCard.idx == secondCard.idx) // 매칭 한 카드가 같을 경우
        {
             firstCard.DestoryCard();
             secondCard.DestoryCard();
             //successAnim.SetBool("isSuccess", true); // 카드 매칭 성공 anim
        }

        else if (firstCard.idx != secondCard.idx)
        {
            if (firstCard.idx == 5)//폭탄일 경우
            {
                
            }

            firstCard.CloseCard();
            secondCard.CloseCard();

        }

        firstCard = null;
        secondCard = null;
    }
}
