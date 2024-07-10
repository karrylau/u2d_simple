using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    // 玩家数组
    public List<PlayerData> playerDatas = new List<PlayerData>();
    // UI
    public Group[] groups;
    private SqlAccess sql;
    public int Flag=0;
    void Start()
    {
        sql = new SqlAccess("localhost", "3306", "root", "1234", "sys"); // 初始化数据库连接
       
        Debug.Log("场景加载完成");
        CalHighestScore();
        Debug.Log(CalHighestScore());
        UpdateLeaderboardUI();
        restart();
        /*
         * if(Flag == 2)
        {   
             
    }*/
    }


 /*    public IEnumerator LoadLeaderboardData()
    {
        Debug.Log("DATA:" + Flag);
        yield return new WaitForSeconds(1); // 模拟加载延迟
        Debug.Log("DATA:" + Flag);
        SqlAccess.Instance.OpenConnection();
        playerDatas = SqlAccess.Instance.GetLeaderboardData(); // 从数据库获取数据
        SqlAccess.Instance.CloseConnection();
        foreach (var playerData in playerDatas)
        {
            Debug.Log($"Player: {playerData.playerName}, Score: {playerData.playerHighestScore}, Kills: {playerData.playerKillNum}");
        }
        SortScoreBubble(); // 默认按照分数排序
        UpdateLeaderboardUI(); // 更新UI
        Flag++;
       
    }*/
    public void restart()
    {
        SqlAccess.Instance.OpenConnection();
        playerDatas = SqlAccess.Instance.GetLeaderboardData(); // 从数据库获取数据
        SqlAccess.Instance.CloseConnection();
        foreach (var playerData in playerDatas)
        {
            Debug.Log($"Player: {playerData.playerName}, Score: {playerData.playerHighestScore}, Kills: {playerData.playerKillNum}");
        }
        SortScoreBubble(); // 默认按照分数排序
        UpdateLeaderboardUI(); // 更新UI
    }

    private string CalHighestScore()// 计算最高分
    {
        Debug.Log("TOP:" + Flag);
        int highestScore = 0;
        string topName = "";
        for (int i = 0; i < playerDatas.Count; i++)
        {
            if (playerDatas[i].playerHighestScore > highestScore)
            {
                highestScore = playerDatas[i].playerHighestScore;
                topName = playerDatas[i].playerName;
            }
        }
        Flag++;
        return topName;
    }

    // 冒泡排序(根据score排序）
    public void SortScoreBubble()
    {
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].playerHighestScore < playerDatas[j + 1].playerHighestScore)
                {
                    PlayerData tmp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = tmp;
                }
            }
        }
        UpdateLeaderboardUI();
    }

    // 冒泡排序（根据killNum排序）
    public void SortKillBubble()
    {
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].playerKillNum < playerDatas[j + 1].playerKillNum)
                {
                    PlayerData tmp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = tmp;
                }
            }
        }
        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI()
    {
        Debug.Log("UPDATE:" + Flag);
        for (int i = 0; i < groups.Length && i < playerDatas.Count; i++)
        {
            groups[i].playerData = playerDatas[i];
            groups[i].updateGroup();
        }
        // 输出更新后的UI信息到控制台
        for (int i = 0; i < groups.Length && i < playerDatas.Count; i++)
        {
            Debug.Log($"Group {i} - Player: {groups[i].playerData.playerName}, Score: {groups[i].playerData.playerHighestScore}, Kills: {groups[i].playerData.playerKillNum}");
        }
        Flag++;
    }
    public void LoadScenestart()
    {
        SceneManager.LoadScene(0);
    }
}
