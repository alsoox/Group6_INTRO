using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn : MonoBehaviour
{
    public GameObject boardObject;
    public void round()
    {
        Board board = boardObject.GetComponent<Board>();
        if (GameManager.Instance.round < 3)
        {
            GameManager.Instance.round++;  // round �� ����
            board.RandomCards(GameManager.Instance.round); // ������ round ���� �Ѱ���
        }
    }
}
