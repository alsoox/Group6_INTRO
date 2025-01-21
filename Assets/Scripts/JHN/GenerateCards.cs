using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCards : MonoBehaviour
{
    public GameObject cardPrefab; // �������� ������ ����
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
            // ������ �ν��Ͻ�ȭ
            GameObject card = Instantiate(cardPrefab);

            // �̸� ����
            card.name = $"Card{i}";
        }
    }
}
