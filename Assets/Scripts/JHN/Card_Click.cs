using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Click : MonoBehaviour
{

    public MixCard card;
    public void OnCardclick()
    {
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = card;
            Debug.Log("firstCardClik");
        }
        else
        {
            GameManager.Instance.secondCard = card;
            GameManager.Instance.Matched();
            Debug.Log("secondCardClik");
        }
    }
}
