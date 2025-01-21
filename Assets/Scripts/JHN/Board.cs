using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject[] cardGroup;
    private GameObject nowCardGroup;

    public int round = 1;
    private int[] arr;

    void Start()
    {
        RandomCards(round);

    }

    void RandomCards(int curRound)
    {
        if (curRound == 1)
        {
            int[] round1 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10 };
            round1 = round1.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round1;

            int g = Random.Range(0, 3);
            nowCardGroup = cardGroup[g];

        }
        if (curRound == 2)
        {
            int[] round2 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10, 10, 10 };
            round2 = round2.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round2;

            int g = Random.Range(3, 6);
            nowCardGroup = cardGroup[g];
        }
        if (curRound == 3)
        {
            int[] round3 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            round3 = round3.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round3;

            int g = Random.Range(6, 9);
            nowCardGroup = cardGroup[g];
        }



        nowCardGroup.SetActive(true);

        // CardGroup0�� �ڽĵ鿡 �ش��ϴ� ī�� ��ġ �迭�� ��������
        for (int i = 0; i < nowCardGroup.transform.childCount; i++)
        {
            // �ڽ� ī�� ����
            Transform child = nowCardGroup.transform.GetChild(i);
            MixCard mixCard = child.GetComponent<MixCard>();


            if (curRound == 3) //���� 3�϶� ����
            {
                if (arr[i] <= 4)
                {
                    mixCard.Setting(arr[i], 3);  // �Ϲ� ī��
                }
                else if (arr[i] <= 9)
                {
                    int a = arr[i] - 5;
                    mixCard.Setting(a, 4);  // �Ϲ� ī��
                }
                else
                {
                    mixCard.Setting();  // ��ź ī��
                }

            }
            else
            {
                // �Ϲ� ī�� �Ǵ� ��ź ī�� ����
                if (arr[i] != 10)
                    mixCard.Setting(arr[i], round);  // �Ϲ� ī��
                else
                    mixCard.Setting();  // ��ź ī��

            }

        }
    }
}
