using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn : MonoBehaviour
{
    public GameObject boardObject;
    public void round()
    {

        Board board = boardObject.GetComponent<Board>();
        if (board.round < 3)
        {
            board.round++;  // round �� ����
            board.RandomCards(board.round); // ������ round ���� �Ѱ���
        }
    }
}
