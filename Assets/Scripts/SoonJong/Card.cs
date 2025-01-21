using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front; // ī�� �ո� �̹��� ��������Ʈ �߰�
    public GameObject back;  // ī�� �޸� �̹��� ��������Ʈ �߰�

    public Animator cardOpenAnim;//ī����� ������̼��߰�

    SpriteRenderer frontImage;

    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number;
        //frontImage.sprite = Resources.Load<Sprite>($"User{idx}"); // Ȯ�� �ʿ� �������� �� ����
    }

    public void OpenCard() 
    {
        //cardOpenAnim.SetBool("isOpen".true); // CardOpen anim �߰�
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
        //cardOpenAnim.SetBool("isClose".true); //CardClose anim �߰�
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
