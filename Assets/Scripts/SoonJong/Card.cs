using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx;
    public int squareSize = 1;

    public GameObject front; // 카드 앞면 이미지 스프라이트 추가
    public GameObject back;  // 카드 뒷면 이미지 스프라이트 추가

    public Animator cardOpenAnim;//카드오픈 에디메이션추가

    public SpriteRenderer frontImage;

    void Awake()
    {
    
    }

    void Start()
    {
       
    }

  
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"UserPicture/Stage1_{idx}"); // 확인 필요 스테이지 1 스테이지 변경 필요
 
    }


    public void OpenCard() 
    {
        //cardOpenAnim.SetBool("isOpen".true); // CardOpen anim 추가
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }

    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        //cardOpenAnim.SetBool("isClose".true); //CardClose anim 추가
        front.SetActive(false);
        back.SetActive(true);
    }

    public void DestoryCard()
    {
        Invoke("DestoryCardInvoke", 0.5f);
    }

    void DestoryCardInvoke()
    {
        Destroy(gameObject);
    }
}
