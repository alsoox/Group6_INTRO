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
            Debug.Log("러시안 룰렛 진행 중! 카드를 클릭할 수 없습니다.");
            return; // 함수 종료
        }
        if (!card.m_isFliping)
        {
            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = card;
                Debug.Log("firstCardClik");
                card.CardOpen();
            }
            else
            {
                // 같은 카드를 두 번 클릭한 경우 무시
                if (GameManager.Instance.firstCard == card)
                {
                    Debug.Log("같은 카드를 두 번 클릭했음");
                    return;
                }

                GameManager.Instance.secondCard = card;
                Debug.Log("secondCardClik");
                card.CardOpen();
                GameManager.Instance.Matched();
            }
        }
    }
}
