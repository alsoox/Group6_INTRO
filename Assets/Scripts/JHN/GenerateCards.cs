using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCards : MonoBehaviour
{
    public GameObject cardPrefab; // 프리팹을 연결할 변수
    public int cardCount = 15;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < cardCount; i++)
        {
            // 프리팹 인스턴스화
            GameObject card = Instantiate(cardPrefab);

            // 이름 변경
            card.name = $"Card{i}";
        }
    }
}
