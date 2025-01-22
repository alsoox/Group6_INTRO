using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject[] cardGroup;
    private GameObject nowCardGroup;


    public GameObject[] cards; // ī����� ��� �迭
    public Vector3[] targetPositions; // ī����� �̵��� ��ǥ ��ġ

    public int round = 1;
    private int[] arr;

    void Start()
    {
        //RandomCards(round);
    }

    private IEnumerator AnimateCardsToPosition()    //ī�� �ִϸ��̼�
    {
        float moveDuration = 0.05f; // �̵� �ð�
        float timeElapsed;

        // �� ī�帶�� �̵�
        for (int i = 0; i < cards.Length; i++)
        {
            Transform cardTransform = cards[i].transform;
            Vector3 startPosition = cardTransform.position;
            Vector3 targetPosition = targetPositions[i];

            timeElapsed = 0;
            while (timeElapsed < moveDuration)
            {
                cardTransform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);   // ��������
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            cardTransform.position = targetPosition; // (Lerp��� ������)���� ��ġ�� ����
        }
    }
    public void RandomCards(int curRound)
    {
        if (curRound == 1)
        {
            int[] round1 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10 };
            round1 = round1.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round1;

            int g = Random.Range(0, 3);
            nowCardGroup = cardGroup[g];

        }
        else if (curRound == 2)
        {
            int[] round2 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10, 10, 10 };
            round2 = round2.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round2;

            int g = Random.Range(3, 6);
            nowCardGroup = cardGroup[g];
        }
        else if (curRound == 3)
        {
            int[] round3 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            round3 = round3.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round3;

            int g = Random.Range(6, 9);
            nowCardGroup = cardGroup[g];
        }
        else{
            Debug.Log("wrong curRound");
            return;
        }

        nowCardGroup.SetActive(true);

        // ī�� �� ��ǥ ��ġ �ʱ�ȭ
        int cardCount = nowCardGroup.transform.childCount;
        cards = new GameObject[cardCount];
        targetPositions = new Vector3[cardCount];


        // CardGroup0�� �ڽĵ鿡 �ش��ϴ� ī�� ��ġ �迭�� ��������
        for (int i = 0; i < cardCount; i++)
        {
            // �ڽ� ī�� ����
            Transform child = nowCardGroup.transform.GetChild(i);
            cards[i] = child.gameObject;

            targetPositions[i] = child.position;     // ��ǥ ��ġ�� ���� ī�� ��ġ�� ����

            child.position = new Vector3(0, 0, 0);  // �ʱ� ��ġ ����

            Card mixCard = child.GetComponent<Card>();

            if (curRound == 3) //���� 3�϶� ����
            {
                if (arr[i] <= 4)
                {
                    mixCard.Setting(arr[i], 3);  // �Ϲ� ī�� (_3)
                }
                else if (arr[i] <= 9)
                {
                    int a = arr[i] - 5;
                    mixCard.Setting(a, 4);  // �Ϲ� ī�� (_4)
                }
                else
                {
                    mixCard.Setting();  // ��ź ī��
                }

            }
            else
            {
                if (arr[i] != 10)
                    mixCard.Setting(arr[i], round);  // �Ϲ� ī��
                else
                    mixCard.Setting();  // ��ź ī��
            }
            if (arr[i] > 4 && arr[i] <= 9) mixCard.index = arr[i] - 5;
            else mixCard.index = arr[i];
        }
        StartCoroutine(AnimateCardsToPosition());   // ī����� ��ǥ ��ġ�� ������ ��, �ִϸ��̼� ����
    }
}
