using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class close_Button : MonoBehaviour
{
    public Button close_button;
    public GameObject Rank_Board;


    public void click_close_button()
    {
        Rank_Board.SetActive(false);
    }

    public void newtest()
    {
        Rank_Board.GetComponent<Rankborad>().AddNewPlayerScore("NewPlayer", 750);
    }
}
