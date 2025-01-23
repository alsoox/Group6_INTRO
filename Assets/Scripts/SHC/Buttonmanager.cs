using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttonmanager : MonoBehaviour
{
    public Button Button;
    public GameObject Rank_Board;
    public GamestartControll GameStartcontroll;
    public GameObject boardObject;
    public GameObject input_name;
    public InputField playerNameInput;
    private string playerName = null;

    public void GameStart()//게임?�� ?��?��
    {
        input_name.SetActive(true);
        playerNameInput.onEndEdit.AddListener(OnNameEntered);
        //?��기까�?�? ?��름입?��   
    }

    public void CreditBtn()//?��?��?��?�� ?��?��
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void RankingBtn()//?��?��버튼
    {
        Rank_Board.SetActive(true);
    }

    public void Gameend()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void get_name()
    {
        // playerName?�� GameManager?�� user_name?�� ????��
        playerName = playerNameInput.text;
        GameManager.Instance.user_name = playerName;
        input_name.SetActive(false);

        goto_GameStart();
    }
    private void OnNameEntered(string text)
    {
        // ?��?��?���? ?��?��?��?��?�� ?�� playerName?�� ????��
        if (text.Length > 0&&text.Length<6)
        {
            get_name();
        }
        else
        {
            GameObject alert = input_name.transform.Find("Alert").gameObject; // alert?�� input_name?�� ?��?�� 객체�? �??��
            alert.SetActive(true);
            StartCoroutine(HideAlertAfterDelay(alert));
        }


    }
    private IEnumerator HideAlertAfterDelay(GameObject alert)
    {
        yield return new WaitForSeconds(1f); // 2�? ?��
        alert.SetActive(false);
    }
    public void goto_GameStart()
    {
        GameStartcontroll.StartGame();
        Board board = boardObject.GetComponent<Board>();

        GameManager.Instance.round = 1;  // round �? 증�??
        board.RandomCards(GameManager.Instance.round); // 증�???�� round 값을 ?��겨줌

    }

    public void goto_Menu()
    {
        GameManager.Instance.RoundInitialize();
        GameManager.Instance.GameInit();
        GameStartcontroll.GoingToMenu();
        SceneManager.LoadScene("MainScene");

    }
}
