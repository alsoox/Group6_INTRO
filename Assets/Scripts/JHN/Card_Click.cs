using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Click : MonoBehaviour
{

    public Card card;
    public void OnCardclick()
    {
        if (GameManager.Instance.RussianRouletteAction.m_isAction)
        {
            Debug.Log("���þ� �귿 ���� ��! ī�带 Ŭ���� �� �����ϴ�.");
            return; // �Լ� ����
        }

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = card;
            Debug.Log("firstCardClik");
            card.CardOpen();
        }
        else
        {
            GameManager.Instance.secondCard = card;
            Debug.Log("secondCardClik");
            card.CardOpen();
            GameManager.Instance.Matched();
        }
    }
}
