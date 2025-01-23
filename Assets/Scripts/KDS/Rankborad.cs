using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

[System.Serializable]
public class Ranking
{
    public string name;
    public int score;
}

[System.Serializable]
public class RankingList
{
    public Ranking[] ranking;
}

public class Rankborad : MonoBehaviour
{

    public Text[] rankingTexts = new Text[10];  // Text UI 배열 (1~10등을 표시할 텍스트)
    private RankingList rankingList;
    private string jsonFilePath = "Assets/Resources/JSON/Rank.json";  // JSON 파일 경로

    void Start()
    {
        RankboradInitialize();
    }

    public void AddNewPlayerScore(string p_name,int p_score)
    {
        // 새로운 점수를 랭킹에 추가
        Ranking newPlayer = new Ranking { name = p_name, score = p_score };
        // 기존 랭킹에 추가
        if (newPlayer.score > rankingList.ranking[9].score)
        {
            Debug.Log("점수가높지요");
            var updatedRankingList = rankingList.ranking.ToList();
            updatedRankingList.Add(newPlayer);

            // 점수 내림차순으로 정렬
            updatedRankingList = updatedRankingList.OrderByDescending(r => r.score).ToList();

            // 10명만 유지
            if (updatedRankingList.Count > 10)
            {
                updatedRankingList.RemoveAt(10);
            }

            // 랭킹 리스트 갱신
            rankingList.ranking = updatedRankingList.ToArray();

            // JSON 파일에 새로운 랭킹 저장
            string updatedJson = JsonUtility.ToJson(rankingList, true);
            File.WriteAllText(jsonFilePath, updatedJson);

            // UI에 실시간으로 순위 반영
            DisplayRanking();
         }
        else
        {
            Debug.Log("점수가낮지요");
        }
    }

    void DisplayRanking()
    {
        for (int i = 0; i < rankingTexts.Length; i++)
        {
            if (i < rankingList.ranking.Length)
            {
                rankingTexts[i].text = (i + 1) + ". " + rankingList.ranking[i].name + " - " + rankingList.ranking[i].score;
            }
            else
            {
                rankingTexts[i].text = (i + 1) + ". -";
            }
        }
    }
    public void RankboradInitialize()
    {
        // JSON 파일 읽기
        string json = File.ReadAllText(jsonFilePath);
        // JSON 파싱
        rankingList = JsonUtility.FromJson<RankingList>(json);
        // UI에 랭킹 출력
        DisplayRanking();
    }
}
