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
    public GameObject GoMainButton;
    private string playerName = null;

    public void GameStart()//게임씬 이동
    {
        input_name.SetActive(true);
        playerNameInput.onEndEdit.AddListener(OnNameEntered);
        //여기까지가 이름입력   



    }

    public void CreditBtn()//크레딧씬 이동
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void RankingBtn()//랭킹버튼
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
        // playerName을 GameManager의 user_name에 저장
        playerName = playerNameInput.text;
        GameManager.Instance.user_name = playerName;
        input_name.SetActive(false);

        goto_GameStart();
    }
    private void OnNameEntered(string text)
    {
        // 텍스트가 입력되었을 때 playerName에 저장
        if (text.Length > 0&&text.Length<6)
        {
            get_name();
        }
        else
        {
            GameObject alert = input_name.transform.Find("Alert").gameObject; // alert는 input_name의 자식 객체로 가정
            alert.SetActive(true);
            StartCoroutine(HideAlertAfterDelay(alert));
        }


    }
    private IEnumerator HideAlertAfterDelay(GameObject alert)
    {
        yield return new WaitForSeconds(1f); // 2초 후
        alert.SetActive(false);
    }
    public void goto_GameStart()
    {
        GameStartcontroll.StartGame();
        Board board = boardObject.GetComponent<Board>();

        GameManager.Instance.round = 1;  // round 값 증가
        board.RandomCards(GameManager.Instance.round); // 증가된 round 값을 넘겨줌

        GoMainButton.SetActive(true);

    }

    public void goto_Menu()
    {
        GameManager.Instance.RoundInitialize();
        GameManager.Instance.GameInit();
        GameStartcontroll.GoingToMenu();

        // GoMainButton을 메인 화면에서는 비활성화
        GoMainButton.SetActive(false);
        SceneManager.LoadScene("MainScene");

    }
}
