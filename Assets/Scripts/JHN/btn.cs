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
            board.round++;  // round 값 증가
            board.RandomCards(board.round); // 증가된 round 값을 넘겨줌
        }
    }
}
